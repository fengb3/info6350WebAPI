namespace info6350WebAPI;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("[controller]")]
public class ProductPostController : ControllerBase
{
    private readonly IDB<ProductPost> _db;

    public ProductPostController(IDB<ProductPost> db)
    {
        _db = db;
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] ProductPost productPost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the product exists
        if (!_db.TryGet(productPost.ProductId, out _))
        {
            return BadRequest("Invalid ProductId");
        }

        // Check if the company exists
        if (!_db.TryGet(productPost.CompanyId, out _))
        {
            return BadRequest("Invalid CompanyId");
        }

        // Check if the product type exists
        if (!_db.TryGet(productPost.ProductTypeId, out _))
        {
            return BadRequest("Invalid ProductTypeId");
        }

        // Check the date format
        if (!DateTime.TryParseExact(productPost.PostedDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            return BadRequest("Invalid PostedDate format. Use yyyy-MM-dd.");
        }

        _db.Insert(productPost);

        return Created($"/ProductPost/{productPost.Id}", productPost);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out var productPost)) return NotFound();

        return Ok(productPost);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] ProductPost productPost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the product exists
        if (!_db.TryGet(productPost.ProductId, out _))
        {
            return BadRequest("Invalid ProductId");
        }

        // Check if the company exists
        if (!_db.TryGet(productPost.CompanyId, out _))
        {
            return BadRequest("Invalid CompanyId");
        }

        // Check if the product type exists
        if (!_db.TryGet(productPost.ProductTypeId, out _))
        {
            return BadRequest("Invalid ProductTypeId");
        }

        // Check the date format
        if (!DateTime.TryParseExact(productPost.PostedDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            return BadRequest("Invalid PostedDate format. Use yyyy-MM-dd.");
        }

        _db.Update(productPost);

        return Ok(productPost);
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
        var productPosts = _db.GetAll();

        if (!productPosts.Any()) return NoContent();

        return Ok(productPosts);
    }
}