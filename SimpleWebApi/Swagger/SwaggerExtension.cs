using MediatR;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SimpleWebApi.Database.Extensions;
using SimpleWebApi.Infrastructure.Exceptions.Base;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleWebApi.Swagger;

public static class SwaggerExtension
{

    private static readonly Dictionary<Type, OpenApiSchema> ApiSchemata = [];
    private static List<Type> _types = [];


    public static void InitTypes(params Type[] flags)
    {
        _types = flags?.SelectMany(x => x.Assembly.GetTypes())
            ?.Where(x => x is { IsClass: true, IsAbstract: false } && typeof(IBaseRequest).IsAssignableFrom(x))?.ToList() ?? [];
    }


    public static void GenerateSchema(this DocumentFilterContext context)
    {
        foreach (var type in _types)
        {
            GetOrAddTypeToSchema(context, GetResponseType(type));
            GetOrAddTypeToSchema(context, type);
        }

        GetOrAddTypeToSchema(context, typeof(string));
    }

    private static void GetOrAddTypeToSchema(DocumentFilterContext context, Type? type)
    {
        if (type is null || ApiSchemata.Any(x=>x.Key == type) ) return;

        var schema = context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository);

        ApiSchemata.TryAdd(type, schema);
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
    private static void AddPath(this OpenApiDocument document, Type request)
    {
        OpenApiPathItem pathItem = new()
        {
            Operations = new Dictionary<OperationType, OpenApiOperation>()
        };

        var responseType = GetResponseType(request);

        var group = request!.Namespace!.Split('.').Last();

        var operation = new OpenApiOperation()
        {
            Tags = [new OpenApiTag() { Name = group }],
            RequestBody = CreateRequestBody(request),
            Responses = CreateResponse(responseType)
        };

        pathItem.Operations.Add(OperationType.Post, operation);

        pathItem.Parameters = [CreateHeader("NameSpace", request.Namespace)];

        document.Paths.Add($"/Commands/{request.Name}", pathItem);
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
        if (responseType is null) return null;
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