using Microsoft.EntityFrameworkCore;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<User> Users { get; set; }
    DbSet<Link> Links { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
