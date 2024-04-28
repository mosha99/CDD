using MediatR;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Requests.Bicycles;
using SimpleWebApi.Requests.Cars;

namespace SimpleWebApi.Handler.Bicycles;

public class AddBicycleCommandHandler(IWriteRepository<Bicycle, BicycleId> repository, ICustomMapper mapper) : IRequestHandler<AddBicycleCommand, long>
{
    public async Task<long> Handle(AddBicycleCommand request, CancellationToken cancellationToken)
    {
        var bicycle = mapper.MapTo<AddBicycleCommand, Bicycle>(request);
        var result = await repository.AddAsync(bicycle, cancellationToken);
        await repository.SaveAsync(cancellationToken);
        return result;
    }
}