using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLink.Application.Features.Users.Commands.UploadImage;

public record UploadImageCommand(Guid userId, IFormFile file) : IRequest<Result<string>>;
public class UploadImageCommandHandler(IFileService fileService, IApplicationDbContext context) : IRequestHandler<UploadImageCommand, Result<string>>
{
    IFileService _fileService = fileService;
    IApplicationDbContext _context = context;
    public async Task<Result<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.userId);

        if (user == null) return Result<string>.Failure(new Error("user", "User not found!"));

        var UrlResult = await fileService.SaveImageAsync(request.file);

        if (!UrlResult.IsSuccess) return UrlResult;

        user.ImageUrl = UrlResult.Value;

        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return UrlResult;
    }
}
