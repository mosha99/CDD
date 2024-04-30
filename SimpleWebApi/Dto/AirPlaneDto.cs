using SimpleWebApi.Domain.AirPlaneDomain;
 

namespace SimpleWebApi.Dto;

public class AirPlaneDto
{
    public AirPlaneId Id { get; set; } = null!;
    public string? Name { set; get; }
}