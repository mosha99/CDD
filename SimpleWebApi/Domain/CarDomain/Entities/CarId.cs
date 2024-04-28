using System.Text.Json.Serialization;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Domain.CarDomain.Entities;

[method: JsonConstructor]
public class CarId(long id) : IdentityBase(id), IIdentityCreator
{ 
    public static IdentityBase CreateInstance(long id) =>new CarId(id);
}