using Aplication.Interfaces;
using Infrastruture.Services;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserNamesList, UserList>();
        return services;
    }
}