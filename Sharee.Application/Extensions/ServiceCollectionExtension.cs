using Sharee.Application.Services;

namespace Sharee.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSharingOption(this IServiceCollection serviceCollection, SharingServiceOption option)
    {
        return serviceCollection.AddSingleton(option);
    }
    
    public static IServiceCollection AddSharingOption(this IServiceCollection serviceCollection, String uploadPrefix, String downloadPrefix, String pathToSharingFolder)
    {
        return serviceCollection.AddSingleton(new SharingServiceOption(uploadPrefix, downloadPrefix, pathToSharingFolder));
    }
}