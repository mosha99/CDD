using SimpleWebApi.Domain.AirPlaneDomain;
using SimpleWebApi.Infrastructure.Filter;
 

namespace SimpleWebApi.Filters;

public class AirPlaneFilter : BaseFilter<AirPlane>
{
    public long? Id { get; set; }
    public string? Name { set; get; }

    public override void InitExpression()
    {
        AddExpression(Id, x => x.ID == new AirPlaneId(Id!.Value));
        AddExpression(Name, x => x.Name.Contains(Name!));
    }
}