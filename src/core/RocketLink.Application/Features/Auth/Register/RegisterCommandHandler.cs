using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.DTOs;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Features.Auth.Register;

public class RegisterCommand : IRequest<Result<AuthResponse>>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterCommandHandler(IApplicationDbContext context, IJwtService jwtService) : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly IApplicationDbContext _context = context;
    private static readonly List<string> Words = new List<string>
    {
        "admin", "signup", "signin"
    };

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!IsValidUsername(request.Username))
        {
            return Result<AuthResponse>.Failure(new Error("username", "Invalid username"));
        }

        var existUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email || x.Username == request.Email);

        if (existUser is not null) return Result<AuthResponse>.Failure(new Error("username", "User already exists"));

        var user = new User()
        {
            Username = request.Username,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GetToken(user);

        return Result<AuthResponse>.Success(new AuthResponse(ObjectMapper.MapGeneric<User, UserDTO>(user), token));
    }

    private bool IsValidUsername(string username)
    {
        if (Words.Contains(username.ToLower()))
        {
            return false;
        }

        if (!username.All(ch => char.IsLower(ch) || char.IsDigit(ch)))
        {
            return false;
        }

        return true;
    }
}
