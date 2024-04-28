using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Infrastructure.Filter;

namespace SimpleWebApi.Filters;

public class BicycleFilter : BaseFilter<Bicycle>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Model { get; set; } = null!;

    public override void InitExpression()
    {
        AddExpression(Id, t => t.ID == new CarId(Id!.Value));
        AddExpression(Name, t => t!.Name!.Equals(Name));
        AddExpression(Model, t => t!.Model!.Equals(Model));
    }
}