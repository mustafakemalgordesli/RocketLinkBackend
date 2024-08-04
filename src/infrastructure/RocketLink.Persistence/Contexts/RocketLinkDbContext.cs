using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Entities;

namespace RocketLink.Persistence.Contexts;

public class RocketLinkDbContext(DbContextOptions<RocketLinkDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Link> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
