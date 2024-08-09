using Microsoft.AspNetCore.Http;
using RocketLink.Domain.Common;

namespace RocketLink.Application.Interfaces;

public interface IFileService
{
    Task<Result<string>> SaveImageAsync(IFormFile file);
}
