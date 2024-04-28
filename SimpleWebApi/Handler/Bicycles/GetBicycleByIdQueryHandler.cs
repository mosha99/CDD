using MediatR;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Dto;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.Mapper;
using SimpleWebApi.Requests.Bicycles;

namespace SimpleWebApi.Handler.Bicycles;

public class GetBicycleByIdQueryHandler(IReadRepository<Bicycle, BicycleId> repository, ICustomMapper mapper) : IRequestHandler<GetBicycleByIdQuery, BicycleDto>
{
    public async Task<BicycleDto> Handle(GetBicycleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(new BicycleId(request.Id), false, cancellationToken);
        return mapper.MapTo<Bicycle, BicycleDto>(result);
    }
}