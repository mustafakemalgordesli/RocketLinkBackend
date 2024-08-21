using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Users.Queries.CheckUsernamelInUse;

public record CheckUsernameInUseQuery(string Username) : IRequest<Result>;

public class CheckUsernameInUseQueryHandler(IApplicationDbContext context) : IRequestHandler<CheckUsernameInUseQuery, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(CheckUsernameInUseQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

        if (user != default) return Result.Failure(new Error("username", "Username is already taken"));

        return Result.Success();
    }
}
