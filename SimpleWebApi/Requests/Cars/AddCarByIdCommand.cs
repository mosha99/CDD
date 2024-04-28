using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Requests.Cars;

public class AddCarCommand : IRequest<long>
{
    public string Name { get; set; } = null!;
    public string Company { get; set; } = null!;
    public int DorCount { get; set; }
}