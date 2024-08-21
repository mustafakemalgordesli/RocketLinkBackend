using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLink.Application.Features.Links.Commands.UpdateLink;

public class UpdateLinkCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public bool? IsActive { get; set; } 
}

public class UpdateLinkCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateLinkCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<Guid>> Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
    {
        var existLink = await _context.Links.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (existLink == null) return Result<Guid>.Failure(new Error("link", "Link not found"));

        existLink = ObjectMapper.UpdateProperties(request, existLink);

        _context.Links.Update(existLink);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(existLink.Id);
    }
}
