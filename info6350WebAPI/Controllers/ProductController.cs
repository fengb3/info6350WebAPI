using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace info6350WebAPI;

// partial class Product

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    
    private readonly IDB<Product> _db;
    
    public ProductController(IDB<Product> db)
    {
        _db = db;
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the company exists
        if (_db.TryGet(product.CompanyId, out _))
        {
            return BadRequest("Invalid CompanyId");
        }

        _db.Insert(product);

        return Created($"/Product/{product.Id}", product);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out var product)) return NotFound();

        return Ok(product);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the company exists
        if (_db.TryGet(product.CompanyId, out _)) return BadRequest("Invalid CompanyId");

        _db.Update(product);

        return Ok(product);
    }

    [HttpDelete("delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out _)) return NotFound();

        _db.Delete(id);
        
        return Ok();
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var products = _db.GetAll();

        if (!products.Any()) return NoContent();

        return Ok(products);
    }
}