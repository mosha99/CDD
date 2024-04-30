using Microsoft.Extensions.Primitives;
using System.Text.Json;
using MediatR;
using SimpleWebApi.Infrastructure.Exceptions;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using System.Text.Json.Serialization;

namespace SimpleWebApi.Executor;

public class CommandExecutor(ISender sender)
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };
    public async Task<object?> Execute(string commandName, JsonElement request, HttpRequest httpRequest)
    {
        if (httpRequest.Headers.TryGetValue("NameSpace", out StringValues nameSpace))
        {
            commandName = $"{nameSpace}.{commandName}";
        }

        var command = CreateCommand(commandName, request);

        //You Can Here Implement Command Validator By Type

        //You Can Here Implement Command Authorization By Type

        return await sender.Send(command);
    }
    private static object CreateCommand(string commandName, JsonElement request)
    {
        var commandType = typeof(Program).Assembly.GetType(commandName);

        if (commandType == null || !typeof(IBaseRequest).IsAssignableFrom(commandType))
            throw new CommandNotFountException();

        var command = request.Deserialize(commandType, SerializerOptions);

        return command ?? throw new CanNotDeserializeCommandException();
    }
}