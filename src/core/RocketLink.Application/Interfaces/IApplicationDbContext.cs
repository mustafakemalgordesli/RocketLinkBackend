using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<User> Users { get; set; }
    DbSet<Link> Links { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
    DatabaseFacade Database { get; }
}
