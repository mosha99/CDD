using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;
using SimpleWebApi.Database.Extensions;
using SimpleWebApi.Database.ValueComparers;
using SimpleWebApi.Database.ValueConverters;
using SimpleWebApi.Database.ValueGenerators;
using SimpleWebApi.Domain.BicycleDomain.Entities;
using SimpleWebApi.Domain.CarDomain.Entities;
using SimpleWebApi.Infrastructure.DomainInfra.AggregateBase;
using SimpleWebApi.Infrastructure.DomainInfra.DbContextBase;
using SimpleWebApi.Infrastructure.DomainInfra.EntityBase;
using SimpleWebApi.Infrastructure.DomainInfra.IdBase;

namespace SimpleWebApi.Database;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext()
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Bicycle>? Bicycles { get; set; }
    public DbSet<Car>? Cars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureEntities(modelBuilder);
    }
    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var type = entityType.ClrType;

            var typeId = type.GetProperty(nameof(IAggregate<IdentityBase>.ID));

            if (typeId is null) continue;

            typeof(AppDbContext).CallMethod(nameof(InitKey), [type, typeId.PropertyType], modelBuilder);
        }
    }
    public static void InitKey<TEntity, TId>(ModelBuilder modelBuilder)
        where TId : IdentityBase, IIdentityCreator
        where TEntity : Aggregate<TId>
    {
        string sequenceName = IdentityBase.GetSequenceBase<TId>();

        modelBuilder.HasSequence<long>(sequenceName).StartsAt(1000).IncrementsBy(1);
        modelBuilder
            .Entity<TEntity>(e =>
            {
                e.HasKey(x => x.ID);
                e.Property(x => x.ID)
                    .HasConversion<TypedIdValueConverter<TId>, TypedIdValueComparer<TId>>()
                    .HasValueGenerator<IdValueGenerator<TId>>();
            });
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        BeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        BeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    void BeforeSaving()
    {
        var changedEntity = ChangeTracker.Entries<IEntity>();
        foreach (var entityEntry in changedEntity)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entityEntry.Entity.Version += 1;
                    entityEntry.Entity.LastModified = DateTime.Now;
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Entity.IsDeleted = true;
                    entityEntry.Entity.Version += 1;
                    entityEntry.Entity.LastModified = DateTime.Now;
                    break;
            }
        }
    }
    public IQueryable<T> EntitySet<T>(bool tracking = false) where T : class
    {
        IQueryable<T> result = Set<T>();

        if (tracking) result = result.AsTracking();
        else result = result.AsNoTracking();

        return result;
    }

    async ValueTask IDbContext.AddAsync<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken) where TAggregate : class
    {
       await this.AddAsync(aggregate,cancellationToken);
    }

    Task<int> IDbContext.SaveAsync(CancellationToken cancellationToken) => this.SaveChangesAsync(cancellationToken);
}