using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Domain.BicycleDomain.Entities;

public class BicycleId(long id) : IdentityBase(id), IIdentityCreator
{
    public static IdentityBase CreateInstance(long id) => new BicycleId(id);
}