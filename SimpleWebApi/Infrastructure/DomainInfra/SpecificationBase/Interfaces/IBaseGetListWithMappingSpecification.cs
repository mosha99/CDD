using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;

public interface IBaseGetListWithMappingSpecification<in TEntity, TTarget>
{
    public Task<ListResult<TTarget>> GetAllAsync(IQueryable<TEntity> queryable, CancellationToken cancellationToken);
}