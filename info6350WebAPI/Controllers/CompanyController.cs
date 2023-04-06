using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace info6350WebAPI;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly IDB<Company> _db;

    public CompanyController(IDB<Company> db)
    {
        _db = db;
    }
    
    [HttpPost("add")]
    public IActionResult Add([FromBody, Required] Company company)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _db.Insert(company);

        return Created($"/Company/{company.Id}", company);
    }

    [HttpGet("get/{id:long}")]
    public IActionResult Get(long id)
    {
        var company = _db.Get(id);

        if (company != null)
        {
            return Ok(company);
        }

        return NotFound();
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody, Required] Company company)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _db.Update(company);

        return Ok(company);
    }

    [HttpDelete("delete/{id:long}")]
    public IActionResult Delete(long id)
    {
        var company = _db.Get(id);

        if (company == null) return NotFound();

        _db.Delete(id);

        return Ok();
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        var companies = _db.GetAll();

        if (!companies.Any()) return NoContent();

        return Ok(companies);
    }
}