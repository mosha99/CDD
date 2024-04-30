using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Implementations;

public class WriteRepository<TAggregate, TId>(IDbContext dataBaseContext) : IWriteRepository<TAggregate, TId>
    where TId : IdentityBase, IIdentityCreator
    where TAggregate :Aggregate<TId>
{
    protected readonly IDbContext DataBaseContext = dataBaseContext;

    public async Task<TId> AddAsync(TAggregate aggregate, CancellationToken cancellationToken = new CancellationToken())
    {
        await DataBaseContext.AddAsync(aggregate, cancellationToken);
        return aggregate.ID;
    }

    public TAggregate Update(TAggregate aggregate) 
    {
         DataBaseContext.UpdateEntity(aggregate);
         return aggregate;
    }

    public async Task<TId> RemoveAsync(TId id, CancellationToken cancellationToken = new CancellationToken())
    {
         await DataBaseContext.EntitySet<TAggregate>().Where(x=>x.ID == id).ExecuteDeleteAsync(cancellationToken);
         return id;
    }

    public Task<int> SaveAsync(CancellationToken cancellationToken = new CancellationToken()) => DataBaseContext.SaveAsync(cancellationToken);
}