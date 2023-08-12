using Npgsql;

namespace Sharee.Application.Extensions;

public static class ConfigurationExtension
{
    public static IConfigurationBuilder AddShareeConfiguration(this IConfigurationBuilder configuration, String fileName = "dbcontext.json") 
        => configuration.AddJsonFile(fileName);

    public static String GetShareeConnectionString(this IConfiguration configuration)
    {
        var builder = new NpgsqlConnectionStringBuilder();

        builder.Host = configuration.GetValue<String>(nameof(builder.Host));
        builder.Port = configuration.GetValue<Int32?>(nameof(builder.Port)) ?? NpgsqlConnection.DefaultPort;

        builder.Database = configuration.GetValue<String>(nameof(builder.Database));
        
        builder.Username = configuration.GetValue<String>(nameof(builder.Username));
        builder.Password = configuration.GetValue<String>(nameof(builder.Password));
        
        return builder.ConnectionString;
    }
}