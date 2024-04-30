using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;

namespace SimpleWebApi.Domain.AirPlaneDomain;

public class AirPlane : Aggregate<AirPlaneId>
{
    public string Name { set; get; } = null!;
}