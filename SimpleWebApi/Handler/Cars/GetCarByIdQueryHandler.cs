using MediatR;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Requests.Cars;

namespace SimpleWebApi.Handler.Cars;
public class GetCarByIdQueryHandler(IReadRepository<Car, CarId> repository, ICustomMapper mapper) : IRequestHandler<GetCarByIdQuery, CarDto>
{
    public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(new CarId(request.Id),false,cancellationToken);
        return mapper.MapTo<Car, CarDto>(result);
    }
}