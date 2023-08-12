using Microsoft.AspNetCore.Mvc;

namespace Sharee.Application.Interfaces;

public interface ISharingService<in TBase> where TBase : IBase
{
    protected static string? UploadPrefix { get; }
    protected static string? DownloadPrefix { get; }
    
    Task UploadBaseAsync(IFormFile file, TBase @base);
    Task DownloadBaseAsync(HttpContext context, TBase @base);
}