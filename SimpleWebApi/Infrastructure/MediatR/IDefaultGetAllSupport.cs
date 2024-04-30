using MediatR;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Implementation;
using SimpleWebApi.Infrastructure.Filter;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Infrastructure.Response;
 

namespace SimpleWebApi.Infrastructure.MediatR;

public interface IDefaultGetAllSupport<TAggregate, TFilter, TDto, TId> : IDefaultCommand
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
    where TFilter : BaseFilter<TAggregate>
{
    public class GetAllQueryHandler(IReadRepository<TAggregate, TId> repository, ICustomMapper mapper) : IRequestHandler<IGetAllCommand<TAggregate, TFilter, TDto>, ListResult<TDto>>
    {
        public Task<ListResult<TDto>> Handle(IGetAllCommand<TAggregate, TFilter, TDto> request, CancellationToken cancellationToken)
        {
            return repository.GetAllByCondition(new DefaultGetSpecification<TAggregate, TDto, TFilter>(request, mapper), cancellationToken);
        }
    }
}
