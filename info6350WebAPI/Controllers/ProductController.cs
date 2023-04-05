using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace info6350WebAPI;

// partial class Product

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the company exists
        if (DB<Company>.TryGet(product.CompanyId, out _))
        {
            return BadRequest("Invalid CompanyId");
        }

        DB<Product>.Insert(product);

        return Created($"/Product/{product.Id}", product);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!DB<Product>.TryGet(id, out var product)) return NotFound();

        return Ok(product);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the company exists
        if (DB<Company>.TryGet(product.CompanyId, out _)) return BadRequest("Invalid CompanyId");

        DB<Product>.Update(product);

        return Ok(product);
    }

    [HttpDelete("delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!DB<Product>.TryGet(id, out _)) return NotFound();

        DB<Product>.Delete(id);
        return Ok();
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var products = DB<Product>.GetAll();

        if (!products.Any()) return NoContent();

        return Ok(products);
    }
}