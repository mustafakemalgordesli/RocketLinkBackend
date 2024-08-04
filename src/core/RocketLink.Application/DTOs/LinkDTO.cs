namespace RocketLink.Application.DTOs;

public class LinkDTO
{
    public string Title { get; set; }
    public Guid UserId { get; set; }
    public string Url { get; set; }
    public string? IconUrl { get; set; }
    public bool IsActive { get; set; }
}
