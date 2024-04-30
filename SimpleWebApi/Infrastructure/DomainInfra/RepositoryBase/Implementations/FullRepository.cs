using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;
using SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;
using SimpleWebApi.Infrastructure.DomainInfra.SpecificationBase.Interfaces;
using SimpleWebApi.Infrastructure.Response;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Implementations;

public class FullRepository<TAggregate, TId> : IFullRepository<TAggregate, TId>
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public FullRepository(IDbContext dataBaseContext)
    {
        DataBaseContext = dataBaseContext;
        _readRepository = new Lazy<IReadRepository<TAggregate, TId>>(() => new ReadRepository<TAggregate, TId>(DataBaseContext));
        _writeRepository = new Lazy<IWriteRepository<TAggregate, TId>>(() => new WriteRepository<TAggregate, TId>(DataBaseContext)); ;
    }
    protected IDbContext DataBaseContext { get; private init; }

    private readonly Lazy<IReadRepository<TAggregate, TId>> _readRepository;
    private readonly Lazy<IWriteRepository<TAggregate, TId>> _writeRepository;

    public Task<TId> AddAsync(TAggregate aggregate, CancellationToken cancellationToken = new CancellationToken())
        => _writeRepository.Value.AddAsync(aggregate, cancellationToken);

    public TAggregate Update(TAggregate aggregate)
        => _writeRepository.Value.Update(aggregate);

    public Task<TId> RemoveAsync(TId id, CancellationToken cancellationToken = new CancellationToken())
        => _writeRepository.Value.RemoveAsync(id, cancellationToken);
    public Task<int> SaveAsync(CancellationToken cancellationToken = new CancellationToken())
        => _writeRepository.Value.SaveAsync(cancellationToken);

    public Task<TAggregate> GetByIdAsync(TId id, bool track = false,
        CancellationToken cancellationToken = new CancellationToken())
        => _readRepository.Value.GetByIdAsync(id, track, cancellationToken);

    public Task<TAggregate?> GetSingleByCondition(IBaseGetSpecification<TAggregate> specification,
        CancellationToken cancellationToken = new CancellationToken())
        => _readRepository.Value.GetSingleByCondition(specification, cancellationToken);

    public Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetAggregateListSpecification<TAggregate> specification,
        CancellationToken cancellationToken = new CancellationToken())
        => _readRepository.Value.GetAllByCondition(specification, cancellationToken);

    public Task<ListResult<TTarget>> GetAllByCondition<TTarget>(
        IBaseGetListWithMappingSpecification<TAggregate, TTarget> specification,
        CancellationToken cancellationToken = new CancellationToken())
        => _readRepository.Value.GetAllByCondition<TTarget>(specification, cancellationToken);

    [Obsolete("Don`t use this in DDD architecture")]
    public Task<ListResult<TAggregate>> GetAllByCondition(IBaseGetEntityListSpecification<TAggregate> specification,
        CancellationToken cancellationToken = new CancellationToken())
        => _readRepository.Value.GetAllByCondition(specification, cancellationToken);
}