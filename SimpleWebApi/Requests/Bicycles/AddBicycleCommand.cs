using MediatR;

namespace SimpleWebApi.Requests.Bicycles;

public class AddBicycleCommand : IRequest<long>
{
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
}