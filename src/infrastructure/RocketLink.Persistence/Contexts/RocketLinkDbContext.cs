using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;
using System.Reflection;

namespace RocketLink.Persistence.Contexts;

public class RocketLinkDbContext(DbContextOptions<RocketLinkDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IApplicationDbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public DbSet<User> Users { get; set; }
    public DbSet<Link> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);
        PrepareAddedEntities(addedEntities);
    }

    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.IsDeleted == default)
                entity.IsDeleted = false;
            if (entity.CreatedAt == DateTime.MinValue)
                entity.CreatedAt = DateTime.Now;
        }
    }
}
