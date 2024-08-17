using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Features.Links.Commands.UpdatePriority;

public record UpdatePriorityCommand(List<Guid> ids) : IRequest<Result>;
public class UpdatePriorityCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdatePriorityCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(UpdatePriorityCommand request, CancellationToken cancellationToken)
    {
        var links = await _context.Links
            .Where(link => request.ids.Contains(link.Id))
            .ToListAsync(cancellationToken);

        for (int i = 0; i < request.ids.Count; i++)
        {
            var link = links.FirstOrDefault(l => l.Id == request.ids[i]);
            if (link != null)
            {
                link.Priority = i + 1; 
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
