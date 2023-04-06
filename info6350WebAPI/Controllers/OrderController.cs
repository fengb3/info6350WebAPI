using System.ComponentModel.DataAnnotations;

namespace info6350WebAPI;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;

// ...

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IDB<Order> _db;

    public OrderController(IDB<Order> db)
    {
        _db = db;
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] Order order)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the product post exists
        if (!_db.TryGet(order.PostId, out _))
        {
            return BadRequest("Invalid PostId");
        }

        // Check if the product exists
        if (!_db.TryGet(order.ProductId, out _))
        {
            return BadRequest("Invalid ProductId");
        }

        // Check if the product type exists
        if (!_db.TryGet(order.ProductTypeId, out _))
        {
            return BadRequest("Invalid ProductTypeId");
        }

        // Check the date format
        if (!DateTime.TryParseExact(order.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            return BadRequest("Invalid Date format. Use yyyy-MM-dd.");
        }

        _db.Insert(order);

        return Created($"/Order/{order.Id}", order);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out var order)) return NotFound();

        return Ok(order);
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] Order order)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Check if the product post exists
        if (!_db.TryGet(order.PostId, out _))
        {
            return BadRequest("Invalid PostId");
        }

        // Check if the product exists
        if (!_db.TryGet(order.ProductId, out _))
        {
            return BadRequest("Invalid ProductId");
        }

        // Check if the product type exists
        if (!_db.TryGet(order.ProductTypeId, out _))
        {
            return BadRequest("Invalid ProductTypeId");
        }

        // Check the date format
        if (!DateTime.TryParseExact(order.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
        {
            return BadRequest("Invalid Date format. Use yyyy-MM-dd.");
        }

        _db.Update(order);

        return Ok(order);
    }

    [HttpDelete("delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_db.TryGet(id, out _)) return NotFound();

        _db.Delete(id);
        
        return Ok();
    }
}
