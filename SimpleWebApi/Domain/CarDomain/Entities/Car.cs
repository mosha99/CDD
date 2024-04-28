using System.ComponentModel.DataAnnotations;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;

namespace SimpleWebApi.Domain.CarDomain.Entities;

public class Car : Aggregate<CarId>
{
    [StringLength(50)] public string Name { get; set; } = null!;
    [StringLength(50)] public string Company { get; set; } = null!;
    public int DorCount { get; set; }

}