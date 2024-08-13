using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Users.Commands.RemoveUser;

public record RemoveUserCommand(Guid id) : IRequest<Result>;
public class RemoveUserCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveUserCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.id);

        if (user == null) return Result<Guid>.Failure(new Error("user", "User not found"));
        
        user.IsDeleted = true;
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
