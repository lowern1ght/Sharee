using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Controllers.Api;

[ApiController]
[Route("v1/[controller]/[action]")]
public class UnitController : Controller
{
    private readonly ShareeDbContext _context;
    private readonly ILogger<UnitController> _logger;

    public UnitController(ShareeDbContext context, ILogger<UnitController> logger)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet]
    [ActionName("list")]
    [ProducesResponseType(typeof(IEnumerable<Unit>), 200)]
    public async Task<ActionResult<IEnumerable<Unit>>> ListAsync()
    {
        return await _context.Units.ToListAsync();
    }
    
    [HttpPost]
    [ActionName("add")]
    public async Task<ActionResult> AddAsync([FromBody] Unit unit)
    {
        if (!ModelState.IsValid)
        {
            return Problem(detail: $"{nameof(Unit)} not valid");
        }

        if (_context.Units.Any(u => u.Id == unit.Id))
        {
            return Problem(detail: $"{unit.Id} exists in base");
        }

        await _context.Units.AddAsync(unit);

        try
        {
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "{Entity} saved", unit.ToString());
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            return Problem(exception.Message);
        }

        return Ok();
    }
    
    [HttpPut]
    [ActionName("update")]
    public async Task<ActionResult> UpdateAsync([FromBody] Unit unit)
    {
        if (!ModelState.IsValid)
        {
            return Problem(detail: $"{nameof(Unit)} not valid");
        }

        if (_context.Units.Any(u => u.Id == unit.Id))
        {
            return Problem(detail: $"{unit.Id} exists in base");
        }

        _context.Units.Update(unit);

        try
        {
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Information, "{Entity} updated", unit.ToString());
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            return Problem(exception.Message);
        }

        return Ok();
    }
    
    
}