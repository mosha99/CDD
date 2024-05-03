using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;

public interface IBaseGetEntityListSpecification<TResult>
{
    public Task<ListResult<TResult>> GetAllAsync(IReadDbContext queryable, CancellationToken cancellationToken);
}