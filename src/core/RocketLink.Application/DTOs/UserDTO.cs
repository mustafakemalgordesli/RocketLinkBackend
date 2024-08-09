namespace RocketLink.Application.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string? Fullname { get; set; }
    public string? ImageUrl { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}
