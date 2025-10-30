using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

public class Presupuestos
{
    // private int idPresupuesto;
    // private string? nombreDestinatario;
    // private string? fechaCreacion;
    // private List<PresupuestoDetalle> detalle = new();

    public int IdPresupuestos { get ; set ; }
    public string? NombreDestinatario { get ; set; }
    public string? FechaCreacion { get ; set ; }
    public List<PresupuestoDetalle> Detalle { get ; set; }

    public float MontoPresupuesto(int id)
    {
        if (id != idPresupuesto)
            return 0;


        float montoTotal = detalle.Sum(d => d.Producto.Precio * d.Cantidad);

        return montoTotal;
    }


    public float MontoPresupuestoConIva(int id)
    {
        float iva = 1.21F;
        float montoConIva = MontoPresupuesto(id) * iva;

        return (montoConIva);
    }
    
   public int CantidadProductos(int id)
    {
        int totalProductos = detalle.Sum(p => p.Cantidad);
        return (totalProductos);
    }
}

