using System.Reflection;
using Microsoft.Extensions.Primitives;
using System.Text.Json;
using MediatR;
using SimpleWebApi.Infrastructure.Exceptions;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using System.Data;
using Azure.Core;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Requests.AirPlane;
using SimpleWebApi.Infrastructure.Validations;
using System.ComponentModel;

namespace SimpleWebApi.Executor;

public class CommandExecutor(ISender sender)
{ 
    public async Task<object?> Execute(HttpRequest request)
    {
        string commandName = GetCommandName(request);

        var command= await CreateCommand(commandName, request);

        var propertyInfos = command.GetType().GetProperties();

        ValidateRequestHttpMethod(command, request);

        SetPropertyFromQueryString(command,propertyInfos, request);

        SetPropertyFromPath(command,propertyInfos, request);

        return await sender.Send(command);
    }

    private void ValidateRequestHttpMethod(object command, HttpRequest request)
    {
        var method = command.GetType().GetCustomAttribute<HttpMethodAttribute>()?.Method ?? HttpMethodEnum.Post;

        if(string.Equals(request.Method, method.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
            string.Equals(request.Method, HttpMethodEnum.Post.ToString(), StringComparison.CurrentCultureIgnoreCase) ) return;

        throw new MethodNotAllowedException();
    }
    private static string GetCommandName(HttpRequest request, string? commandName = null)
    {
        commandName ??= request.Path.ToString().Split('/')[2];

        if (request.Headers.TryGetValue("NameSpace", out StringValues nameSpace) || request.Query.TryGetValue("NameSpace".ToLower(), out nameSpace))
        {
            commandName = $"{nameSpace}.{commandName}";
        }

        return commandName;
    }
    private static async Task<object> CreateCommand(string commandName, HttpRequest request)
    {
        var commandType = typeof(Program).Assembly.GetType(commandName);

        if (commandType == null || !typeof(IBaseRequest).IsAssignableFrom(commandType))
            throw new CommandNotFountException();

        object? result;

        if (request.ContentLength > 0)
        {
            result = await request.ReadFromJsonAsync(commandType);
        }
        else
        {
           result = Activator.CreateInstance(commandType) ?? throw new CanCreateCommandException();
        }
        
        return result!;
    }
    private static void SetPropertyFromPath(object result, PropertyInfo[] properties, HttpRequest request)
    {
        foreach (var prop in properties)
        {
            var index = prop.GetCustomAttribute<FromPathAttribute>()?.Index;

            if (index is null) continue;

            var pathPart = request.Path.ToString().Split('/');

            if (pathPart.Length - 1 < 3 + index.Value) return;

            var value = pathPart[3 + index.Value];

            TypeConverter typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);

            object propValue = typeConverter.ConvertFromString(value)!;

            prop.SetValue(result, propValue);
        }
    }
    private static void SetPropertyFromQueryString(object result, PropertyInfo[] properties, HttpRequest request)
    {
        foreach (var item in request.Query)
        {
            var prop = properties.FirstOrDefault(x => x.Name.ToLower().Equals(item.Key.ToLower()));
            if (prop == null) continue;

            TypeConverter typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);

            object propValue = typeConverter.ConvertFromString(item.Value)!;

            prop.SetValue(result, propValue);
        }
    }
}