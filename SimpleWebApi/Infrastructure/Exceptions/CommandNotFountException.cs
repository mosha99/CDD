using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class CommandNotFountException(string message = "Command Not Fount") : Exception(message), IException
{
    public int GetStatsCode() => 400;

    public string GetTitle() => "Command Not Fount";

    public string GetMessage() => base.Message;
}