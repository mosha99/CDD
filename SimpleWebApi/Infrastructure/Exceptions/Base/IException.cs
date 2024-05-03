namespace SimpleWebApi.Infrastructure.Exceptions.Base;

public interface IException
{
    public int GetStatsCode();
    public string GetTitle();
    public object GetResult();
}