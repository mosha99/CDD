using SimpleWebApi.Domain.BicycleDomain.Entities;

namespace SimpleWebApi.Dto;

public class BicycleDto 
{
    public BicycleId Id { get; set; }
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
}