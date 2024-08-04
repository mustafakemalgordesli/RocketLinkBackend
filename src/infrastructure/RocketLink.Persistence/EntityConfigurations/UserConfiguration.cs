using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Entities;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Persistence.EntityConfigurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(255);

        builder.ToTable("users", RocketLinkDbContext.DEFAULT_SCHEMA);
    }
}
