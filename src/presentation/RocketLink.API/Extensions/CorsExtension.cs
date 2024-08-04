namespace RocketLink.API.Extensions;

public static class CorsExtension
{
    public static void ConfigureCors(this IServiceCollection services, string MyAllowSpecificOrigins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:3000", 
                                      "http://127.0.0.1:3000")
                                  .AllowAnyMethod()
                                  .AllowCredentials()
                                  .AllowAnyHeader();
                              });
        });
    }
}
