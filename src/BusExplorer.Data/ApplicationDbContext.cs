using BusExplorer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BusExplorer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceBusConnection> ServiceBusConnections { get; set; }
}
