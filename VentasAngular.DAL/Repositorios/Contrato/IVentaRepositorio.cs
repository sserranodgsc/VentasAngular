using VentasAngular.Model;

namespace VentasAngular.DAL.Repositorios.Contrato
{
    public interface IVentaRepositorio : IGenericRepositorio<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
