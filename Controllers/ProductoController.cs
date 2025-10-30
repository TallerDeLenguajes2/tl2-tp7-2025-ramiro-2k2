using Microsoft.AspNetCore.Mvc;
namespace TP6.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    private ProductoRepository _productoRepository;

    public ProductoController()
    {
        _productoRepository = new ProductoRepository();
    }

    [HttpGet("products")]
    public IActionResult GetAll()
    {
        var prductos = _productoRepository.GetAll();
        return Ok(productos);

    }   

}