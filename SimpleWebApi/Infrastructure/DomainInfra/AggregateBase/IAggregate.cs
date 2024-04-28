using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;

public interface IAggregate<out TId> : IEntity
    where TId : IdentityBase
{
    TId ID { get; }
}