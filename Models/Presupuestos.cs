namespace TP07.Models;

public class Presupuestos
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; } = string.Empty;
    public string FechaCreacion { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    public List<PresupuestoDetalle> Detalle { get; set; } = new();

    public float MontoPresupuesto()
        => Detalle.Sum(d => d.Producto.Precio * d.Cantidad);

    public float MontoPresupuestoConIva()
        => MontoPresupuesto() * 1.21f;

    public int CantidadProductos()
        => Detalle.Sum(p => p.Cantidad);
}
