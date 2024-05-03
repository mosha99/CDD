using System.Diagnostics;
using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SimpleWebApi.Database.Extensions;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.Exceptions.Base;
using SimpleWebApi.Requests.AirPlane;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleWebApi.Swagger;

public static class SwaggerExtension
{

    private static readonly Dictionary<Type, OpenApiSchema> ApiSchemata = [];
    private static List<Type> _types = [];


    public static void InitTypes(params Assembly[] assemblies)
    {
        _types = assemblies?.SelectMany(x => x.GetTypes())
            ?.Where(x => x is { IsClass: true, IsAbstract: false } && typeof(IBaseRequest).IsAssignableFrom(x))?.ToList() ?? [];
    }


    public static void GenerateSchema(this DocumentFilterContext? context)
    {
        _context = context;
        foreach (var type in _types)
        {
            GetOrAddTypeToSchema(GetResponseType(type));
            GetOrAddTypeToSchema(type);
        }

        GetOrAddTypeToSchema(typeof(string));


    }

    private static DocumentFilterContext? _context;
    private static void GetOrAddTypeToSchema(Type? type)
    {
        if (type is null || ApiSchemata.Any(x => x.Key == type)) return;

        var schema = _context!.SchemaGenerator.GenerateSchema(type, _context.SchemaRepository);

        ApiSchemata.TryAdd(type, schema);
        _context.SchemaRepository.Schemas.TryAdd(type.Name, schema);

    }

    private static Type? GetResponseType(Type type)
    {
        var responseType = type.GetInterfaces().FirstOrDefault(x => x.Name.Equals(typeof(IRequest<>).Name));
        if (responseType == null) return null;
        return responseType.GenericTypeArguments[0];
    }

    public static void FillPaths(this OpenApiDocument document)
    {
        document.Paths = new()
        {
            Extensions = null
        };
        foreach (var type in _types)
        {
            document.AddPath(type);
        }
    }
    public static OperationType TranslateHttpMethod(HttpMethodEnum method)
    {
        return method switch
        {
            HttpMethodEnum.Post => OperationType.Post,
            HttpMethodEnum.Get => OperationType.Get,
            HttpMethodEnum.Put => OperationType.Put,
            HttpMethodEnum.Delete => OperationType.Delete,
            _ => OperationType.Post
        };
    }

    public static OperationType GetOperationType(Type requestType)
    {
        var httpMethod = requestType.GetCustomAttribute<HttpMethodAttribute>()?.Method ?? HttpMethodEnum.Post;
        return TranslateHttpMethod(httpMethod);
    }

    private static Dictionary<PropertyInfo, int>? _pathParameter;
    private static List<PropertyInfo>? _queryParameter;
    private static OperationType _operationType;
    private static void AddPath(this OpenApiDocument document, Type requestType)
    {
        var path = RutePathFactory(requestType);

        _operationType = GetOperationType(requestType);



        OpenApiPathItem pathItem = new()
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>()
        };

        var responseType = GetResponseType(requestType);

        var group = requestType!.Namespace!.Split('.').Last();

        var operation = new OpenApiOperation()
        {
            Tags = [new OpenApiTag() { Name = group }],
            RequestBody = CreateRequestBody(requestType),
            Responses = CreateResponse(responseType)
        };

        pathItem.Operations.Add(_operationType, operation);

        pathItem.Parameters = 
            [
                CreateHeader("NameSpace", requestType.Namespace),
                ..CreatesPathParameter()
            ];

        document.Paths.Add(path, pathItem);
    }

    private static List<OpenApiParameter> CreatesPathParameter()
    {
        var result = new List<OpenApiParameter>();

        foreach (var item in _pathParameter!)
        {
            GetOrAddTypeToSchema(item.Key.PropertyType);

            ApiSchemata.TryGetValue(item.Key.PropertyType, out var schema);

            var param = new OpenApiParameter()
            {
                In = ParameterLocation.Path ,
                Name = item.Key.Name,
                Schema = schema,
                Required = true,
            };

            result.Add(param);
        }     
        
        foreach (var item in _queryParameter!)
        {
            GetOrAddTypeToSchema(item.PropertyType);

            ApiSchemata.TryGetValue(item.PropertyType, out var schema);

            var param = new OpenApiParameter()
            {
                In = ParameterLocation.Query,
                Name = item.Name,
                Schema = schema,
                Required = true,
            };

            result.Add(param);
        }

        return result;
    }

    public static string RutePathFactory(Type requestType)
    {
        string result = $"/Commands/{requestType.Name}";

        _pathParameter = requestType.GetProperties()
             .Where(x => x.GetCustomAttribute<FromPathAttribute>() != null)
             .ToDictionary(x => x, y => y.GetCustomAttribute<FromPathAttribute>()!.Index);


        _queryParameter = requestType.GetProperties()
             .Where(x => x.GetCustomAttribute<FromQueryStringAttribute>() != null).ToList();


        foreach (var item in _pathParameter.OrderBy(x => x.Value))
        {
            result += $"/{{{item.Key.Name}}}";
        }

        return result;
    }

    private static OpenApiResponses? CreateResponse(Type? responseType = null, string code = "200", string description = "Success")
    {
        if (responseType is null) return null;

        var response = new OpenApiResponses();

        ApiSchemata.TryGetValue(responseType, out var schema);

        response.Add(code, new OpenApiResponse()
        {
            Description = description,
            Content =
            {
                new KeyValuePair<string, OpenApiMediaType>("application/json", new OpenApiMediaType()
                {
                    Schema = schema
                })
            }
        });

        return response;
    }

    private static OpenApiRequestBody? CreateRequestBody(Type? responseType, string? description = null)
    {
        if (responseType is null || _operationType is OperationType.Get || _operationType is OperationType.Delete) return null;
        ApiSchemata.TryGetValue(responseType, out var schema);

        var response = new OpenApiRequestBody()
        {
            Description = description,
            Content =
            {
                new KeyValuePair<string, OpenApiMediaType>("application/json", new OpenApiMediaType()
                {
                    Schema = schema,
                })
            }
        };

        return response;
    }
    private static OpenApiParameter CreateHeader(string name, string? value = null)
    {
        var response = new OpenApiParameter()
        {

            Name = name,
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema()
            {
                Default = new OpenApiString(value)
            }
        };

        return response;
    }
}