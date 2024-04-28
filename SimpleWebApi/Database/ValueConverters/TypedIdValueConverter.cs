using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.Exceptions;

namespace SimpleWebApi.Database.ValueConverters;

public class TypedIdValueConverter<TTypedIdValue>()
    : ValueConverter<TTypedIdValue, long>(id => id.Id, value => Create(value))
    where TTypedIdValue : IdentityBase, IIdentityCreator
{
    private static TTypedIdValue Create(long id) => TTypedIdValue.CreateInstance(id) as TTypedIdValue ?? throw new UnexpectedException();
}