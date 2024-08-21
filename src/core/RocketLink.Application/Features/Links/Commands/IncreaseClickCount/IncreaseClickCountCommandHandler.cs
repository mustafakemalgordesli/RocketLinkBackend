using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Links.Commands.IncreaseClickCount;

public record IncreaseCountCommand(Guid id) : IRequest<Result>;

public class IncreaseClickCountCommandHandler(IApplicationDbContext context) : IRequestHandler<IncreaseCountCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(IncreaseCountCommand request, CancellationToken cancellationToken)
    {
        var link = await _context.Links.FirstOrDefaultAsync(x => x.Id == request.id);

        if (link == null) return Result<Guid>.Failure(new Error("link", "Link not found"));

        link.ClickCount++;

        _context.Links.Update(link);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
