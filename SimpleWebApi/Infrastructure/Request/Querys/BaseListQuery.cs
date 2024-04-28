using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.Filter;

namespace SimpleWebApi.Infrastructure.Request.Querys;

public abstract class BaseListQuery<TEntity,TFilter> where TFilter : BaseFilter<TEntity>
{
    public TFilter? Filter { get; set; }
    public int SkipCount { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
    public OrderType OrderType { get; set; }
}