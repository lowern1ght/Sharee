using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;
using Sharee.Application.Interfaces;

namespace Sharee.Application.Controllers.Api;

[ApiController]
[Route("v1/[controller]/[action]")]
public class SharingController : Controller
{
    private readonly ShareeDbContext _context;
    private readonly ILogger<SharingController> _logger;
    private readonly ISharingService<Unit> _sharingService;

    public SharingController(ShareeDbContext context, ILogger<SharingController> logger, ISharingService<Unit> sharingService)
    {
        _logger = logger;
        _sharingService = sharingService;
        _context = context;
    }
    
    [HttpPost]
    [ActionName("upload")]
    public async Task<IActionResult> UploadAsync([FromQuery] Int32? id, [FromQuery] String? code)
    {
        if (await GetEntityFromQueryAsync(id, code) is not Unit unit)
        {
            _logger.Log(LogLevel.Information, "{id} or {code} incorrect", id, code);
            return NotFound();
        }

        if (!(Request.Form.Files.Count >= 1))
        {
            _logger.Log(LogLevel.Error, "{Count} error", Request.Form.Files.Count);
            return Problem("Count files error");
        }

        var file = Request.Form.Files[0];

        try
        {
            await _sharingService.UploadBaseAsync(file, unit);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message, exception.Data);
            return Problem(exception.Message);
        }

        return Ok();
    }

    [HttpGet]
    [ActionName("download")]
    public async Task<IActionResult> DownloadAsync([FromQuery] Int32? id, [FromQuery] String? code)
    {
        if (await GetEntityFromQueryAsync(id, code) is not Unit unit)
        {
            _logger.Log(LogLevel.Information, "{id} or {code} incorrect", id, code);
            return NotFound();
        }

        try
        {
            await _sharingService.DownloadBaseAsync(HttpContext, unit);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message, exception.Data);
            return Problem(exception.Message);
        }

        return Ok();
    }

    private async Task<IBase?> GetEntityFromQueryAsync(Int32? id, String? code)
    {
        if (!id.HasValue && code is null)
        {
            return null;
        }

        if (id.HasValue)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Id == id.Value);
        }

        if (code != null)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Code == code);
        }
        
        return null;
    }
}