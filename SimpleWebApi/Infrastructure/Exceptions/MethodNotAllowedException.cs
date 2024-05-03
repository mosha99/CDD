using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class MethodNotAllowedException(string message = "Method Not Allowed") : Exception(message), IException
{
    public int GetStatsCode() => 405;

    public string GetTitle() => "Method Not Allowed ";

    public object GetResult() => base.Message;
}