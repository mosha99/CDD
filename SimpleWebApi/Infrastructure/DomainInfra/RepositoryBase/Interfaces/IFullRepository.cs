using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;

public interface IFullRepository<TAggregate, TId> : IWriteRepository<TAggregate, TId>, IReadRepository<TAggregate, TId>
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{

}