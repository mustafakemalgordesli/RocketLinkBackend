using RocketLink.Domain.Common;

namespace RocketLink.Domain.Entities;

public class User : BaseEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Link> Links { get; set; }
}
