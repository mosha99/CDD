using System.ComponentModel.DataAnnotations.Schema;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using System.Text.Json.Serialization;

namespace SimpleWebApi.Domain.AirPlaneDomain;

[method: JsonConstructor()]
public class AirPlaneId(long id) : IdentityBase(id), IIdentityCreator
{
    public static IdentityBase CreateInstance(long id) => new AirPlaneId(id);
}