namespace info6350WebAPI;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// ...

[ApiController]
[Route("[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly IDB<ProductType> _db;

    public ProductTypeController(IDB<ProductType> db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] ProductType productType)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (_db.GetAll().Any(x => x.Name == productType.Name))
        {
            return BadRequest("Name must be unique");
        }

        _db.Insert(productType);

        return Created($"/ProductType/{productType.Id}", productType);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out var productType)) return NotFound();

        return Ok(productType);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] ProductType productType)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (_db.GetAll().Any(x => x.Name == productType.Name && x.Id != productType.Id))
        {
            return BadRequest("Name must be unique");
        }

        _db.Update(productType);

        return Ok(productType);
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
        var productTypes = _db.GetAll();

        if (!productTypes.Any()) return NoContent();

        return Ok(productTypes);
    }
}
