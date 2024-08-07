using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.DTOs;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;
using System.Runtime.Serialization;

namespace RocketLink.Application.Features.Auth.Login;

public class LoginCommand : IRequest<Result<AuthResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler(IApplicationDbContext context, IJwtService jwtService) : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => (x.Email == request.Email || x.Username == request.Email) && x.IsDeleted == false);

        if (user is null) return Result<AuthResponse>.Failure(new Error("User.NotFound", "User not found!"));

        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return Result<AuthResponse>.Failure(new Error("User.PasswordWrong", "Password is wrong!"));

        var token = _jwtService.GetToken(user);

        return Result<AuthResponse>.Success(new AuthResponse(ObjectMapper.MapGeneric<User, UserDTO>(user), token));
    }
}
