using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class CanNotDeserializeCommandException(string message = "Can Not Deserialize Command") : Exception(message), IException
{
    public int GetStatsCode() => 422;

    public string GetTitle() => "Invalid Command Signature";

    public string GetMessage() => base.Message;
}