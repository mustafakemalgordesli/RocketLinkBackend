using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Links.Commands.RemoveLink;

public record RemoveLinkCommand(Guid id) : IRequest<Result>;

public class RemoveLinkCommandHandler(IApplicationDbContext context) : IRequestHandler<RemoveLinkCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(RemoveLinkCommand request, CancellationToken cancellationToken)
    {
        var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == request.id);

        if(link == null) return Result<Guid>.Failure(new Error("link", "Link not found"));

        link.IsDeleted = true;

        _context.Links.Update(link);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
