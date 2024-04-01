using VentasAngular.DAL.DBContext;
using VentasAngular.DAL.Repositorios.Contrato;
using VentasAngular.Model;

namespace VentasAngular.DAL.Repositorios.Implementacion
{
    public class VentaRepositorio : GenericRepositorio<Venta>, IVentaRepositorio
    {
        private readonly DbventaContext _context;

        public VentaRepositorio(DbventaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto productoEncontrado = _context.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        productoEncontrado.Stock = productoEncontrado.Stock - dv.Cantidad;
                        _context.Productos.Update(productoEncontrado);
                    }

                    await _context.SaveChangesAsync();
                    NumeroDocumento correlativo = _context.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _context.NumeroDocumentos.Update(correlativo);
                    await _context.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _context.Venta.AddAsync(modelo);
                    await _context.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaccion.Commit();
                }
                catch
                {
                    transaccion.Rollback();
                }
                return ventaGenerada;
            }
        }


    }
}
