using Sharee.Application.Interfaces;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Services;

public class SharingService : ISharingService<Unit>
{
    private readonly SharingServiceOption _serviceOption;

    public SharingService(SharingServiceOption serviceOption)
    {
        _serviceOption = serviceOption;
    }
    
    public async Task UploadBaseAsync(IFormFile file, Unit @base)
    {
        var path = GetPathToBaseFile(@base, _serviceOption.UploadPrefix);
        await using var fileStream = File.Create(path);
        await file.CopyToAsync(fileStream);
    }

    public Task DownloadBaseAsync(HttpContext context, Unit @base)
    {
        var path = GetPathToBaseFile(@base, _serviceOption.DownloadPrefix);
        return context.Response.SendFileAsync(path);
    }

    private String GetPathToBaseFile(IBase @base, String prefix)
    {
        var fileName = @base.Code + prefix;
        return Path.Combine(_serviceOption.PathToSharingFolder, fileName);
    }
}