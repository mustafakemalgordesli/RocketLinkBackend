using MediatR;
using RocketLink.Application.DTOs;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;
using System.Runtime.Serialization;

namespace RocketLink.Application.Features.Links.Queries.GetAllByUser;

public record GetAllLinkByUserQuery(Guid userId, bool isActive = true) : IRequest<Result<List<LinkDTO>>>;

public class GetAllByUserQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllLinkByUserQuery, Result<List<LinkDTO>>>
{
    private readonly IApplicationDbContext _context = context;

    public Task<Result<List<LinkDTO>>> Handle(GetAllLinkByUserQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Link> queryable = _context.Links.AsQueryable().Where(x => x.IsActive == request.isActive && x.UserId == request.userId && x.IsDeleted == false);

        List<LinkDTO> list = new List<LinkDTO>();

        foreach (var item in queryable.ToList())
        {
            list.Add(ObjectMapper.MapGeneric<Link, LinkDTO>(item));
        }
        
        return Task.FromResult(Result<List<LinkDTO>>.Success(list));
    }
}


