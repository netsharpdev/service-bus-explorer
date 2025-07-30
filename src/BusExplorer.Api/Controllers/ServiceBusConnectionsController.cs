using BusExplorer.Data;
using BusExplorer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusExplorer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceBusConnectionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ServiceBusConnectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceBusConnection>>> GetServiceBusConnections()
    {
        return await _context.ServiceBusConnections.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceBusConnection>> PostServiceBusConnection(ServiceBusConnection serviceBusConnection)
    {
        _context.ServiceBusConnections.Add(serviceBusConnection);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceBusConnections), new { id = serviceBusConnection.Id }, serviceBusConnection);
    }
}
