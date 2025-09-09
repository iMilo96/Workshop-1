using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Backend.Data;
using Workshop.Shared.Entities;

namespace Workshop.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly DataContext _context;

    public EmployeesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Employees.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest("El parámetro de búsqueda no puede estar vacío.");
        }

        var employees = await _context.Employees.
            Where(x => x.FirstName.Contains(term) || x.LastName.Contains(term)).ToListAsync();

        if (employees == null || employees.Count == 0)
        {
            return NotFound("No se encontraron empleados que coincidan con el término de búsqueda.");
        }

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Employee employee)
    {
        _context.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        _context.Remove(employee);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(Employee employee)
    {
        _context.Update(employee);
        await _context.SaveChangesAsync();
        return Ok(employee);
    }
}