namespace Sharee.Application.Interfaces;

public interface IBase
{
    Int32 Id { get; }
    String? Code { get; }
    DateTime? LastUpdateTime { get; }
    DateTime? LastDownloadTime { get; }
}