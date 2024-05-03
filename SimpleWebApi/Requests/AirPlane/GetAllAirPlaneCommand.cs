using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.Attributes;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;
using SimpleWebApi.Infrastructure.Request.Querys;

namespace SimpleWebApi.Requests.AirPlane;

[HttpMethod(HttpMethodEnum.Post)]
public class GetAllAirPlaneCommand : BaseListQuery<Domain.AirPlaneDomain.AirPlane, AirPlaneFilter>, IGetAllCommand<Domain.AirPlaneDomain.AirPlane, AirPlaneFilter, AirPlaneDto>
{
}