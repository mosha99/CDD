using SimpleWebApi.Infrastructure.Filter;
using System.ComponentModel.DataAnnotations;
using SimpleWebApi.Domain.CarDomain.Entities;

namespace SimpleWebApi.Filters;

public class CarFilter : BaseFilter<Car>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Company { get; set; } = null!;
    public int? DorCountFrom { get; set; }
    public int? DorCountTo { get; set; }
    public override void InitExpression()
    {
        AddExpression(Id, t => t.ID == new CarId(Id!.Value));
        AddExpression(Name, t => t!.Name!.Equals(Name));
        AddExpression(Company, t => t!.Company!.Equals(Company));
        AddExpression(DorCountFrom, t => t.DorCount >= DorCountFrom);
        AddExpression(DorCountTo, t => t.DorCount <= DorCountTo);
    }
}