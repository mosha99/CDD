using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Dto;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;

namespace SimpleWebApi.Requests.AirPlane;

[HttpMethod(HttpMethodEnum.Get)]
public class GetAirPlaneCommand : IGetCommand<Domain.AirPlaneDomain.AirPlane, AirPlaneId, AirPlaneDto>
{
    [FromPath(0)]
    public long Id { set; get; }
}