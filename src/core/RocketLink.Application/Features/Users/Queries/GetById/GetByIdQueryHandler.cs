using MediatR;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.DTOs;
using RocketLink.Application.Interfaces;
using RocketLink.Application.Mapping;
using RocketLink.Domain.Common;
using RocketLink.Domain.Entities;

namespace RocketLink.Application.Features.Users.Queries.GetById;


public record GetByIdQuery(Guid id) : IRequest<Result<UserDTO>>;

public class GetByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetByIdQuery, Result<UserDTO>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<UserDTO>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.id);

        if (user is null) return Result<UserDTO>.Failure(new Error("User.NotFound", "User not found!"));

        return Result<UserDTO>.Success(ObjectMapper.MapGeneric<User, UserDTO>(user));
    }
}
