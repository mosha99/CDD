using System.ComponentModel.DataAnnotations;
using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;

public abstract class Aggregate<TId> : Entity, IAggregate<TId> where TId : IdentityBase, IIdentityCreator
{

    [Key]
    public TId ID { get; private set; } = null!;
}