﻿using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Infrastructure.DomainInfra.RepositoryBase.Interfaces;

public interface IWriteRepository
{
    public Task<int> SaveAsync(CancellationToken cancellationToken = new CancellationToken());
}

public interface IWriteRepository<TAggregate, TId> : IWriteRepository
    where TId : IdentityBase, IIdentityCreator
    where TAggregate : Aggregate<TId>
{
    public Task<TId> AddAsync(TAggregate aggregate, CancellationToken cancellationToken = new CancellationToken());
    public TAggregate Update(TAggregate aggregate);
    public Task<TId> RemoveAsync(TId id, CancellationToken cancellationToken = new CancellationToken());
}