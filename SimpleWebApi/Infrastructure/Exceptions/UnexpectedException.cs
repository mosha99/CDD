using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class UnexpectedException() : Exception("Internal Server Error"), IException
{
    public int GetStatsCode() => 500;
    public string GetTitle() => "Internal Server Error";

    public object GetResult() => base.Message;
}