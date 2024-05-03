using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IDefaultAddSupport<TAggregate, TId> : IDefaultCommandHandler
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public class AddCommandHandler(IWriteRepository<TAggregate, TId> repository, ICustomMapper mapper) : IRequestHandler<IAddCommand<TAggregate, TId>, TId>
    {
        public async Task<TId> Handle(IAddCommand<TAggregate, TId> request, CancellationToken cancellationToken)
        {
            var bicycle = mapper.MapTo<TAggregate>(request);
            var result = await repository.AddAsync(bicycle, cancellationToken);
            await repository.SaveAsync(cancellationToken);
            return result;
        }
    }
}