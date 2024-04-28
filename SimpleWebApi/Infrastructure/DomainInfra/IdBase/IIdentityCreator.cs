namespace SimpleWebApi.Infrastructure.DomainInfra.IdBase;

public interface IIdentityCreator
{
    static abstract IdentityBase CreateInstance(long id);
}