using Microsoft.AspNetCore.Mvc;
using TP07.Models;
using TP07.Repositorios;

namespace TP07.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ProductoRepository repository = new();

    [HttpPost]
    public ActionResult Crear(Productos producto)
    {
        repository.Crear(producto);
        return Ok("Producto creado correctamente");
    }

    [HttpPut("{id}")]
    public ActionResult Modificar(int id, Productos producto)
    {
        repository.Modificar(id, producto);
        return Ok("Producto modificado correctamente");
    }

    [HttpGet]
    public ActionResult<List<Productos>> Listar()
    {
        return Ok(repository.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Productos?> Obtener(int id)
    {
        var prod = repository.GetById(id);
        if (prod == null) return NotFound();
        return Ok(prod);
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(int id)
    {
        bool eliminado = repository.Eliminar(id);
        if (!eliminado) return NotFound();
        return NoContent();
    }
}
