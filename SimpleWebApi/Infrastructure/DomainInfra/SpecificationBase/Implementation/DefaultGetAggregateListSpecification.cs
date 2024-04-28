using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;
using SimpleWebApi.Infrastructure.Extencions;
using SimpleWebApi.Infrastructure.Filter;
using SimpleWebApi.Infrastructure.Request.Querys;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Implementation;

public class DefaultGetAggregateListSpecification<TAggregate, TFilter>(BaseListQuery<TAggregate, TFilter> listQuery) : IBaseGetAggregateListSpecification<TAggregate>
    where TFilter : BaseFilter<TAggregate>
{
    public Task<ListResult<TAggregate>> GetAllAsync(IQueryable<TAggregate> queryable, CancellationToken cancellationToken)
    {
        return queryable.ToListResultAsync(listQuery, cancellationToken);
    }
}