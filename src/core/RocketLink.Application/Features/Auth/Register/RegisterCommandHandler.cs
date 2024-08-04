using MediatR;
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

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
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
}
