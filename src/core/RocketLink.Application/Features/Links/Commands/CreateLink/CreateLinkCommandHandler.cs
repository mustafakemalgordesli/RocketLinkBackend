using MediatR;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Features.Links.Commands.CreateLink;

public class CreateLinkCommand : IRequest<Result<Guid>>
{
    public string Title { get; set; }
    public Guid UserId { get; set; }
    public string Url { get; set; }
}

public class CreateLinkCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateLinkCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<Guid>> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var link = ObjectMapper.MapGeneric<CreateLinkCommand, Link>(request);
       
        await _context.Links.AddAsync(link);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(link.Id);
    }
}
