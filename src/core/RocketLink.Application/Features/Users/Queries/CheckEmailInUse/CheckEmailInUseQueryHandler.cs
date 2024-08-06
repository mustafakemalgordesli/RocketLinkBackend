using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Users.Queries.CheckEmailInUse;

public record CheckEmailInUseQuery(string Email) : IRequest<Result>;

public class CheckEmailInUseQueryHandler(IApplicationDbContext context) : IRequestHandler<CheckEmailInUseQuery, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(CheckEmailInUseQuery request, CancellationToken cancellationToken)
    {
        var user = _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user != null) return Result.Failure(new Error("email", "Email is already taken"));

        return Result.Success();
    }
}
