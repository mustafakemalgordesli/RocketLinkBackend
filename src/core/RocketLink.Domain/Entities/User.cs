using RocketLink.Domain.Common;

namespace RocketLink.Domain.Entities;

public class User : BaseEntity
{
    public string? Fullname { get; set; }
    public string? ImageUrl { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int ViewsCount { get; set; } = 0;
    public virtual ICollection<Link> Links { get; set; }
}
