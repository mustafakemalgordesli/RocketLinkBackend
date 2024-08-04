using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RocketLink.Domain.Entities;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Persistence.EntityConfigurations;

public class LinkConfiguration : BaseEntityConfiguration<Link>
{
    public override void Configure(EntityTypeBuilder<Link> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Url).IsRequired();

        builder.ToTable("links", RocketLinkDbContext.DEFAULT_SCHEMA);
    }
}
