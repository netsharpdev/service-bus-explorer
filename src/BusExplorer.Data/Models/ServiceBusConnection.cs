using System.ComponentModel.DataAnnotations;

namespace BusExplorer.Data.Models;

public class ServiceBusConnection
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string ConnectionString { get; set; }
}
