using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocketLink.Application.Behaviors;
using RocketLink.Application.Interfaces;
using RocketLink.Persistence.Contexts;

namespace RocketLink.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RocketLinkDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreConnection")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<RocketLinkDbContext>());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
