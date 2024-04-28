using MediatR;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Implementation;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Infrastructure.Response;
using SimpleWebApi.Requests.Bicycles;

namespace SimpleWebApi.Handler.Bicycles;

public class GetAllBicycleQueryHandler(IReadRepository<Bicycle, BicycleId> repository, ICustomMapper mapper) : IRequestHandler<GetAllBicycleQuery, ListResult<BicycleDto>>
{
    public Task<ListResult<BicycleDto>> Handle(GetAllBicycleQuery request, CancellationToken cancellationToken)
    {
        return repository.GetAllByCondition(new DefaultGetSpecification<Bicycle, BicycleDto, BicycleFilter>(request, mapper), cancellationToken);
    }
}
