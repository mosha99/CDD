using MediatR;
using SimpleWebApi.Dto;

namespace SimpleWebApi.Requests.Bicycles;

public class GetBicycleByIdQuery : IRequest<BicycleDto>
{
    public long Id { get; set; }
}