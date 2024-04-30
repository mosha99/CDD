using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Dto;
using SimpleWebApi.Filters;
using SimpleWebApi.Infrastructure.Enum;
using SimpleWebApi.Infrastructure.MediatR;
using SimpleWebApi.Infrastructure.Request.Querys;

namespace SimpleWebApi.Requests.AirPorts;


public class AddAirPlaneCommand : IAddCommand<AirPlane, AirPlaneId>
{
    public string Name { set; get; } = null!;
}

public class EditAirPlaneCommand : IEditCommand<AirPlane, AirPlaneId>
{
    public long Id { set; get; }
    public string Name { set; get; } = null!;
}
public class DeleteAirPlaneCommand : IDeleteCommand<AirPlane, AirPlaneId>
{
    public long Id { set; get; }
}

public class GetAirPlaneCommand : IGetCommand<AirPlane, AirPlaneId, AirPlaneDto>
{
    public long Id { set; get; }
}
public class GetAllAirPlaneCommand : BaseListQuery<AirPlane, AirPlaneFilter>, IGetAllCommand<AirPlane, AirPlaneFilter, AirPlaneDto>
{
}