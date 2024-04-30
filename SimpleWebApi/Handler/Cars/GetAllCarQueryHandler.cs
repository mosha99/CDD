using MediatR;
 
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Implementation;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Infrastructure.Response;
using SimpleWebApi.Requests.Cars;

namespace SimpleWebApi.Handler.Cars;

public class GetAllCarQueryHandler(IReadRepository<Car, CarId> repository, ICustomMapper mapper) : IRequestHandler<GetAllCarQuery, ListResult<CarDto>>
{
    public Task<ListResult<CarDto>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
    {
        return repository.GetAllByCondition(new DefaultGetSpecification<Car, CarDto, CarFilter>(request, mapper), cancellationToken);
    }
}