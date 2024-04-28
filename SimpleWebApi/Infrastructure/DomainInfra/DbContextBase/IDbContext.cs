using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography;

namespace SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;

public interface IDbContext
{
    public IQueryable<T> EntitySet<T>(bool tracking = false) where T : class;
    ValueTask AddAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken) where TAggregate : class;
    public Task<int> SaveAsync(CancellationToken cancellationToken);
}