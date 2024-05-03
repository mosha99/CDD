using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IDefaultGetSupport<TAggregate, TId, TDto> : IDefaultCommandHandler
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public class GetByIdQueryHandler(IReadRepository<TAggregate, TId> repository, ICustomMapper mapper) : IRequestHandler<IGetCommand<TAggregate, TId, TDto>, TDto>
    {
        public async Task<TDto> Handle(IGetCommand<TAggregate, TId, TDto> request, CancellationToken cancellationToken)
        {
            var result = await repository.GetByIdAsync((TId.CreateInstance(request.Id) as TId)!, false, cancellationToken);
            return mapper.MapTo<TAggregate, TDto>(result);
        }
    }
}