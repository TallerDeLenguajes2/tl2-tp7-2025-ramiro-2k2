using Microsoft.AspNetCore.Mvc;
using TP07.Models;
using TP07.Repositorios;

namespace TP07.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestoController : ControllerBase
{
    private readonly PresupuestoRepository repository = new();

    [HttpPost]
    public ActionResult Crear(Presupuestos presupuesto)
    {
        repository.Crear(presupuesto);
        return Ok("Presupuesto creado correctamente");
    }

    [HttpGet]
    public ActionResult<List<Presupuestos>> Listar()
    {
        return Ok(repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Presupuestos?> Obtener(int id)
    {
        var p = repository.GetById(id);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPost("{id}/ProductoDetalle")]
    public ActionResult AgregarDetalle(int id, [FromBody] PresupuestoDetalle detalle)
    {
        if (detalle == null || detalle.Producto == null)
            return BadRequest("Detalle inválido");
        repository.AgregarProducto(id, detalle.Producto.IdProducto, detalle.Cantidad);
        return Ok("Producto agregado al presupuesto");
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(int id)
    {
        bool eliminado = repository.Eliminar(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}
