using System.ComponentModel.DataAnnotations;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;

namespace SimpleWebApi.Domain.BicycleDomain.Entities;

public class Bicycle : Aggregate<BicycleId>
{
    [StringLength(50)] public string Name { get; set; } = null!;
    [StringLength(50)] public string Model { get; set; } = null!;
}