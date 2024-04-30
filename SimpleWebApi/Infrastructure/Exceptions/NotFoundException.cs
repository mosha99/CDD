using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class NotFoundException(string? message = "object/objects Not Found") : Exception(message), IException
{
    public int GetStatsCode() => 404;

    public string GetTitle() => "object/objects Not Found";

    public string GetMessage() => base.Message;
}