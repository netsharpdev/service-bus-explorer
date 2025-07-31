using BusExplorer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace BusExplorer.Data;

public class ApplicationDbContext : DbContext, IDataProtectionKeyContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceBusConnection> ServiceBusConnections { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}