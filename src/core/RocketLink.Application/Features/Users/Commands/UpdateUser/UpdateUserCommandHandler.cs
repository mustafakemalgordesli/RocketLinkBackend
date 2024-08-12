using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Fullname { get; set; }
    public string? Username { get; set; }
}
public class UpdateUserCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateUserCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updatedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted == false);

        if (updatedUser == null) return Result<Guid>.Failure(new Error("user", "User not found"));

        updatedUser = ObjectMapper.UpdateProperties(request, updatedUser);

        _context.Users.Update(updatedUser);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(updatedUser.Id);
    }
}
