using SimpleWebApi.Infrastructure.Exceptions.Base;

namespace SimpleWebApi.Infrastructure.Exceptions;

public class CanCreateCommandException(string message = "Can Not Create Command") : Exception(message), IException
{
    public int GetStatsCode() => 422;

    public string GetTitle() => "Can Not Create Command";

    public object GetResult() => base.Message;
}