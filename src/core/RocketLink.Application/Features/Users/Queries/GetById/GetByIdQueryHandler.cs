using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.DTOs;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Features.Users.Queries.GetByUsername;

public record GetByUsernameQuery(string username) : IRequest<Result<UserDTO>>;

public class GetByUsernameQueryHandler(IApplicationDbContext context) : IRequestHandler<GetByUsernameQuery, Result<UserDTO>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<UserDTO>> Handle(GetByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.username);

        if (user is null) return Result<UserDTO>.Failure(new Error("User.NotFound", "User not found!"));

        return Result<UserDTO>.Success(ObjectMapper.MapGeneric<User, UserDTO>(user));
    }
}
