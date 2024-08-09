using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocketLink.Application.Interfaces;
using RocketLink.Infrastructure.Services;

namespace RocketLink.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
    }
}
