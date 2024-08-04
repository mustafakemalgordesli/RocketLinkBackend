using RocketLink.Domain.Common;

namespace RocketLink.Domain.Entities;

public class Link : BaseEntity
{
    public string Title { get; set; }
    public Guid UserId { get; set; }
    public string Url { get; set; }
    public string? IconUrl { get; set; }
    public bool IsActive { get; set; } = false;
    public User User { get; set; }
}
