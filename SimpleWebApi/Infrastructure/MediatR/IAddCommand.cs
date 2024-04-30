using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IAddCommand<TEntity, out TId> : IRequest<TId>
    where TEntity : IAggregate<TId>
    where TId : IdentityBase;