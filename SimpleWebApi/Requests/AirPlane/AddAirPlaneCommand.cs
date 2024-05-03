using System.Text.Json.Serialization;
using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;

namespace SimpleWebApi.Requests.AirPlane;

[HttpMethod(HttpMethodEnum.Post)]
public class AddAirPlaneCommand : IAddCommand<Domain.AirPlaneDomain.AirPlane, AirPlaneId>
{
    public string Name { set; get; } = null!;
}