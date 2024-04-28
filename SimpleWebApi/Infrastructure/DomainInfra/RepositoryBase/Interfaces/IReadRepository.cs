using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;

public interface IReadRepository<TAggregate, in TId>
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public Task<TAggregate> GetByIdAsync(TId id, bool track = false, CancellationToken cancellationToken = new CancellationToken());
    public Task<TAggregate?> GetSingleByCondition(IBaseGetSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken());
    public Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetAggregateListSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken());
    public Task<ListResult<TTarget>> GetAllByCondition<TTarget>(IBaseGetListWithMappingSpecification<TAggregate, TTarget> specification, CancellationToken cancellationToken = new CancellationToken());
    [Obsolete("Don`t use this in DDD architecture")]
    public Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetEntityListSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken());
}