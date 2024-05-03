using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class InvalidNumberForCreateIdException : Exception, IException
{
    public InvalidNumberForCreateIdException()
    {
        
    }
    public InvalidNumberForCreateIdException(string message) : base(message)
    {
        
    }

    public int GetStatsCode() => 500;

    public string GetTitle() => "Internal Server Error";

    public object GetResult() => base.Message;
}