 
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using System.ComponentModel.DataAnnotations;
using SimpleWebApi.Domain.CarDomain.Entities;

namespace SimpleWebApi.Dto;

public class CarDto
{
    public CarId Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Company { get; set; } = null!;
    public int DorCount { get; set; }
}