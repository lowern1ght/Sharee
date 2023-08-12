using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Data;

public class ShareeDbContext : DbContext
{
    private readonly string? _connectionString;

    private static class ShareeContextConnection
    {
        public static readonly String DefaultConnectionString 
            = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost", 
                Port = NpgsqlConnection.DefaultPort, 
                Database = "sharee", 
                Username = "sharee_admin", 
                Password = "20Zl$a_zOzlZl102"
            }.ConnectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
            .UseNpgsql(_connectionString ?? ShareeContextConnection.DefaultConnectionString);
    }

    public ShareeDbContext(String? connectionString = null)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Unit> Units { get; set; } = null!;
}