using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentasAngular.DAL.DBContext;

namespace VentasAngular.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventaContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });
        }
    }
}
