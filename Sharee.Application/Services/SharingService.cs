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

    public async Task UploadFileAsync(IFormFile file, Unit unit, String fileExtension)
    {
        var path = GetPathToBaseFile(unit, _serviceOption.UploadPrefix, fileExtension);
        await file.CopyToAsync(File.OpenWrite(path));
    }

    public async Task<String?> GetDownloadFileAsync(Unit unit)
    {
        var fileName = unit.Code + _serviceOption.DownloadPrefix;
        
        foreach (var file in Directory.GetFiles(_serviceOption.PathToSharingFolder))
        {
            if (file.Contains(fileName))
            {
                return file;
            }
        }

        return null;
    }

    private String GetPathToBaseFile(IBase @base, String prefix, String? extension = null)
    {
        var fileName = @base.Code + prefix;
        return Path.Combine(_serviceOption.PathToSharingFolder, fileName + extension);
    }
}