namespace SimpleWebApi.Infrastructure.Mapper;

public interface ICustomMapper
{
    IQueryable<TRight> MapTo<TLeft, TRight>(IQueryable<TLeft> queryable);
    IEnumerable<TRight> MapTo<TLeft, TRight>(IEnumerable<TLeft> collection);
    TRight MapTo<TLeft, TRight>(TLeft entity);
}