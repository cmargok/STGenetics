using Microsoft.Extensions.DependencyInjection;
using STGenetics.Application.Ports;
using STGenetics.Application.Security;
using STGenetics.Application.Services;
using STGenetics.Application.Services.Interfaces;
using STGenetics.Application.Tools.Settings;
using STGenetics.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGenetics.Api.DI
{

    /// <summary>
    /// Dependency Container static class
    /// </summary>
    public static class InfrastructureDependencyContainer
    {
        /// <summary>
        /// Register the Collection services created by the user
        /// </summary>
        /// <param name="Services"></param>
        /// <param name="config"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection Services, IConfiguration config)
        {
            Services.AddSingleton<DapperContext>(provider =>
            {
                var instance = new DapperContext(config.GetConnectionString("STGeneticsDB")!);
                return instance;

            });

            Services.AddScoped<IAnimalRepository, AnimalRepository>();

            Services.AddScoped<IAnimalService, AnimalService>();

            Services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            Services.AddScoped<ITokenService, TokenService>();



            return Services;
        }
    }
}
