using MediatR;
 
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;
 
using SimpleWebApi.Requests.Cars;

namespace SimpleWebApi.Handler.Cars;

public class AddCarCommandHandler(IWriteRepository<Car, CarId> repository, ICustomMapper mapper) : IRequestHandler<AddCarCommand, long>
{
    public async Task<long> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        var bicycle = mapper.MapTo<AddCarCommand, Car>(request);
        var result = await repository.AddAsync(bicycle, cancellationToken);
        await repository.SaveAsync(cancellationToken);
        return result;
    }
}
