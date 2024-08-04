using RocketLink.Domain.Entities;

namespace RocketLink.Application.DTOs;

public record AuthResponse(UserDTO User, string Token);