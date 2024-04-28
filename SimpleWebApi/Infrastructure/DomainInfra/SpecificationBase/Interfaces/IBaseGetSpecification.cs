namespace SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;

public interface IBaseGetSpecification<T>
{
    public Task<T> GetAsync(IQueryable<T> queryable, CancellationToken cancellationToken);
}