using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;

public interface IBaseGetAggregateListSpecification<TEntity>
{
    public Task<ListResult<TEntity>> GetAllAsync(IQueryable<TEntity> queryable, CancellationToken cancellationToken);
}