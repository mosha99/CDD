using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IDefaultEditSupport<TAggregate, TId> : IDefaultCommandHandler
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public class EditCommandHandler(IWriteRepository<TAggregate, TId> repository, ICustomMapper mapper)
        : IRequestHandler<IEditCommand<TAggregate, TId>, TId>
    {
        public async Task<TId> Handle(IEditCommand<TAggregate, TId> request, CancellationToken cancellationToken)
        {
            var bicycle = mapper.MapTo<TAggregate>(request);
            var result = repository.Update(bicycle);
            await repository.SaveAsync(cancellationToken);
            return result.ID;
        }
    }
}