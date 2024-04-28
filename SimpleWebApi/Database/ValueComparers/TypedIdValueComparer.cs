using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Database.ValueComparers;

public class TypedIdValueComparer<TTypedIdValue>()
    : ValueComparer<TTypedIdValue>((left, right) => left != null && right != null && left.Id.Equals(right.Id), o => o.Id.GetHashCode())
    where TTypedIdValue : IdentityBase, IIdentityCreator;