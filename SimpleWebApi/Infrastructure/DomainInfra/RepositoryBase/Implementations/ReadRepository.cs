using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Database;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;
using SimpleWebApi.Infrastructure.Exceptions;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Implementations;

public class ReadRepository<TAggregate, TId>(IReadDbContext dataBaseContext) : IReadRepository<TAggregate, TId>
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    protected readonly IReadDbContext DataBaseContext = dataBaseContext;
    public async Task<TAggregate> GetByIdAsync(TId id, bool track = false, CancellationToken cancellationToken = new CancellationToken())
    {
        return await DataBaseContext.EntitySet<TAggregate>(track)
            .SingleOrDefaultAsync(x => x.ID.Equals(id), cancellationToken) ?? throw new NotFoundException();
    }
    public async Task<TAggregate?> GetSingleByCondition(IBaseGetSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken())
    {
        return await specification.GetAsync(DataBaseContext.EntitySet<TAggregate>(false), cancellationToken)
               ?? throw new NotFoundException();
    }
    public async Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetAggregateListSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken())
    {
        return await specification.GetAllAsync(DataBaseContext.EntitySet<TAggregate>(false), cancellationToken)
               ?? throw new NotFoundException();
    }
    public async Task<ListResult<TTarget>> GetAllByCondition<TTarget>(IBaseGetListWithMappingSpecification<TAggregate, TTarget> specification, CancellationToken cancellationToken = new CancellationToken())
    {
        return await specification.GetAllAsync(DataBaseContext.EntitySet<TAggregate>(false), cancellationToken)
               ?? throw new NotFoundException();
    }
    [Obsolete("Don`t use this in DDD architecture")]
    public async Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetEntityListSpecification<TAggregate> specification, CancellationToken cancellationToken = new CancellationToken())
    {
        return await specification.GetAllAsync(DataBaseContext, cancellationToken)
               ?? throw new NotFoundException();
    }
}