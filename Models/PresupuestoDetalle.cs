namespace TP07.Models;

public class PresupuestoDetalle
{
    public Productos Producto { get; set; } = new();
    public int Cantidad { get; set; }
}
