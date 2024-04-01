using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VentasAngular.DAL.DBContext;
using VentasAngular.DAL.Repositorios.Contrato;

namespace VentasAngular.DAL.Repositorios.Implementacion
{
    public class GenericRepositorio<TModel> : IGenericRepositorio<TModel> where TModel : class
    {
        private readonly DbventaContext _dbventaContext;

        public GenericRepositorio(DbventaContext dbventaContext)
        {
            _dbventaContext = dbventaContext;
        }

        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null!)
        {
            try
            {
                IQueryable<TModel> queryModelo = filtro == null ? _dbventaContext.Set<TModel>() : _dbventaContext.Set<TModel>().Where(filtro);
                return queryModelo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TModel> Crear(TModel modelo)
        {
            try
            {
                _dbventaContext.Set<TModel>().Add(modelo);
                await _dbventaContext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModel modelo)
        {
            try
            {
                _dbventaContext.Set<TModel>().Update(modelo);
                await _dbventaContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModel modelo)
        {
            try
            {
                _dbventaContext.Set<TModel>().Remove(modelo);
                await _dbventaContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TModel> Obtener(Expression<Func<TModel, bool>>? filtro)
        {
            try
            {
                TModel modelo = await _dbventaContext.Set<TModel>().FirstOrDefaultAsync(filtro);
                return modelo!;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
