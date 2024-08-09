
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RocketLink.Application.Interfaces;
using RocketLink.Domain.Common;

namespace RocketLink.Infrastructure.Services;

public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    public async Task<Result<string>> SaveImageAsync(IFormFile file)
    {
        string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        if (file == null || file.Length == 0)
            return Result<string>.Failure(new Error("upload", "No file uploaded."));

        var fileExtension = Path.GetExtension(file.FileName);

        if (string.IsNullOrEmpty(fileExtension) || !_permittedExtensions.Contains(fileExtension))
            return Result<string>.Failure(new Error("upload", "Invalid file type. Only JPG and PNG files are allowed."));

        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"uploads/{uniqueFileName}";

        return Result<string>.Success(fileUrl);
    }
}
