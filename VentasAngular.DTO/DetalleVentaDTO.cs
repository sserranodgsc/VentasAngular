namespace VentasAngular.DTO
{
    public class DetalleVentaDTO
    {
        public int? IdProducto { get; set; }

        public string? ProductoDescripcion { get; set; }

        public int? Cantidad { get; set; }

        public string? Precio { get; set; }

        public string? Total { get; set; }
    }
}
