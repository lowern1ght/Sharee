using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sharee.Application.Authorization;
using Sharee.Application.Data;
using Sharee.Application.Data.Entities;
using Sharee.Application.Interfaces;
using Sharee.Application.Services;

namespace Sharee.Application.Controllers.Api;

[ApiController]
[Route("v1/[controller]/[action]")]
public class SharingController : Controller
{
    private readonly ShareeDbContext _context;
    private readonly ILogger<SharingController> _logger;
    private readonly ISharingService<Unit> _sharingService;

    private const String ContentFileType = "application/octet-stream";

    public SharingController(ShareeDbContext context, ILogger<SharingController> logger, 
        ISharingService<Unit> sharingService, SharingServiceOption serviceOption)
    {
        _logger = logger;
        _context = context;
        _sharingService = sharingService;
    }
    
    [HttpPost]
    [AuthorizationToken]
    [ActionName("upload")]
    public async Task<IActionResult> UploadAsync([FromQuery] Int32? id, [FromQuery] String? code, Guid? token, IFormFile file)
    {
        if (await GetEntityFromQueryAsync(id, code) is not Unit unit)
        {
            _logger.Log(LogLevel.Information, "{id} or {code} incorrect", id, code);
            return NotFound();
        }

        await _sharingService.UploadFileAsync(file, unit, GetFileExtension(file.FileName));

        unit.LastUpdateTime = DateTime.Now;

        _context.Units.Update(unit);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            return Problem();
        }
        
        return Ok();
    }

    [HttpGet]
    [AuthorizationToken]
    [ActionName("download")]
    public async Task<IActionResult> DownloadAsync([FromQuery] Int32? id, [FromQuery] String? code, Guid? token)
    {
        if (await GetEntityFromQueryAsync(id, code) is not Unit unit)
        {
            _logger.Log(LogLevel.Information, "{id} or {code} incorrect", id, code);
            return NotFound();
        }

        var pathToFile = await _sharingService.GetDownloadFileAsync(unit);

        if (pathToFile is null)
        {
            return NotFound();
        }
        
        unit.LastDownloadTime = DateTime.Now;

        _context.Units.Update(unit);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            return Problem();
        }
        
        return File(System.IO.File.OpenRead(pathToFile), ContentFileType, Path.GetFileName(pathToFile));
    }

    private String GetFileExtension(String fileName)
    {
        var indexDot = fileName.LastIndexOf('.');
        return fileName.Substring(indexDot);
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