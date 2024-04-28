namespace SimpleWebApi.Infrastructure.Response;

public class ListResult<T>
{
    public ListResult(List<T> list)
    {
        List = list ?? [];
    }

    public ListResult()
    {
        List = [];
    }
    public List<T> List { get; set; }
    public int Count { get; set; }
}