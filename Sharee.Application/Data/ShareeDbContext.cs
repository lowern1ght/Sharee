using System.Globalization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sharee.Application.Data.Entities;

namespace Sharee.Application.Data;

public class ShareeDbContext : DbContext
{
    private readonly string? _connectionString;

    private static class ShareeContextConnection
    {
        public static readonly String DefaultConnectionString
            = new SqliteConnectionStringBuilder
            {
                DataSource = "sharee.db",
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ConnectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
            .UseSqlite(_connectionString ?? ShareeContextConnection.DefaultConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>()
            .Property(unit => unit.Token)
            .IsRequired(true);
    }

    public ShareeDbContext(String? connectionString = null)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Unit> Units { get; set; } = null!;
}