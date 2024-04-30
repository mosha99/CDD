using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IGetCommand<TEntity, TId, out TDto> : IRequest<TDto>
    where TEntity : IAggregate<TId>
    where TId : IdentityBase
{
    public long Id { set; get; }
}