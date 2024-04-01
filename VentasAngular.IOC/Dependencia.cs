using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentasAngular.DAL.DBContext;
using VentasAngular.DAL.Repositorios.Contrato;
using VentasAngular.DAL.Repositorios.Implementacion;

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

            services.AddTransient(typeof(IGenericRepositorio<>), typeof(GenericRepositorio<>));
            services.AddScoped<IVentaRepositorio, VentaRepositorio>();
        }
    }
}
