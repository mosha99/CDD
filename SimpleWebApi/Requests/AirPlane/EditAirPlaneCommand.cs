using System.Text.Json.Serialization;
using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;

namespace SimpleWebApi.Requests.AirPlane;

[HttpMethod(HttpMethodEnum.Put)]
public class EditAirPlaneCommand : IEditCommand<Domain.AirPlaneDomain.AirPlane, AirPlaneId>
{
    [FromPath(0)]
    public long Id { set; get; }
    public string Name { set; get; } = null!;
}
