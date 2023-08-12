namespace Sharee.Application.Services;

public class SharingServiceOption
{
    public SharingServiceOption(string uploadPrefix, string downloadPrefix, string pathToSharingFolder)
    {
        this.UploadPrefix = uploadPrefix;
        this.DownloadPrefix = downloadPrefix;
        this.PathToSharingFolder = pathToSharingFolder;
    }

    public String UploadPrefix { get; init; }
    public String DownloadPrefix { get; init; }
    public String PathToSharingFolder { get; init; }
}