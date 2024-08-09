namespace RocketLink.Application.DTOs;

public class LinkDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid UserId { get; set; }
    public string Url { get; set; }
    public string? IconCode { get; set; }
    public bool IsActive { get; set; }
}
