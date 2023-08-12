using Microsoft.AspNetCore.Mvc;

namespace Sharee.Application.Interfaces;

public interface ISharingService<in TBase> where TBase : IBase
{
    public Task<String?> GetDownloadFileAsync(TBase @base);
    public Task UploadFileAsync(IFormFile file, TBase @base, String fileExtension);
}