using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ELI.Data;
using ELI.Data.Context;
using ELI.Entity.Auth;

namespace ELI.API.Configurations
{
    public static class ConfigureConnections
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionMainDb = configuration.GetConnectionString("ELIDb");
            services.AddDbContext<ELIContext>(options => options.UseSqlServer(connectionMainDb));
            var connectionAuthDb = configuration.GetConnectionString("ELIAuthDb");
            services.AddDbContext<ELIAuthDbContext>(options => options.UseSqlServer(connectionAuthDb));
            return services;
        }
    }
}
