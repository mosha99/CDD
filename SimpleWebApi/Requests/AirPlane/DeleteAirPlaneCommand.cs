using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;

namespace SimpleWebApi.Requests.AirPlane;

[HttpMethod(HttpMethodEnum.Delete)]
public class DeleteAirPlaneCommand : IDeleteCommand<Domain.AirPlaneDomain.AirPlane, AirPlaneId>
{
    [FromPath(0)]
    public long Id { set; get; }
}