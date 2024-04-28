using MediatR;
using SimpleWebApi.Dto;

namespace SimpleWebApi.Requests.Cars;

public class GetCarByIdQuery : IRequest<CarDto>
{
    public long Id { get; set; }
}