using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IDefaultDeleteSupport<TAggregate, TId> : IDefaultCommandHandler
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public class DeleteCommandHandler(IWriteRepository<TAggregate, TId> repository) : IRequestHandler<IDeleteCommand<TAggregate, TId>, TId>
    {
        public async Task<TId> Handle(IDeleteCommand<TAggregate, TId> request, CancellationToken cancellationToken)
        {
            var result = await repository.RemoveAsync((TId.CreateInstance(request.Id) as TId)!, cancellationToken);
            await repository.SaveAsync(cancellationToken);
            return result;
        }
    }
}