using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.MediatR;

namespace SimpleWebApi.Handler.AirPort;

public class AirPlaneHandlerAggregate :
    IDefaultAddSupport<AirPlane, AirPlaneId>,
    IDefaultEditSupport<AirPlane, AirPlaneId>,
    IDefaultDeleteSupport<AirPlane, AirPlaneId>,
    IDefaultGetSupport<AirPlane, AirPlaneId, AirPlaneDto>,
    IDefaultGetAllSupport<AirPlane, AirPlaneFilter, AirPlaneDto, AirPlaneId>
{

}