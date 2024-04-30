using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography;

namespace SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;

public interface IDbContext : IReadDbContext
{
}

public interface IReadDbContext : IWriteDbContext
{
    public IQueryable<T> EntitySet<T>(bool tracking = false) where T : class;
}
public interface IWriteDbContext
{
    ValueTask AddAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken) where TAggregate : class;

    EntityEntry<TAggregate> UpdateEntity<TAggregate>(TAggregate aggregate) where TAggregate : class;

    public Task<int> SaveAsync(CancellationToken cancellationToken);
}