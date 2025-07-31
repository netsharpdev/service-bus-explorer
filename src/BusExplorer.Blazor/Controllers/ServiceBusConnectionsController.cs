using BusExplorer.Data;
using BusExplorer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusExplorer.Blazor.Controllers;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutServiceBusConnection(int id, ServiceBusConnection serviceBusConnection)
    {
        if (id != serviceBusConnection.Id)
        {
            return BadRequest();
        }

        _context.Entry(serviceBusConnection).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ServiceBusConnections.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceBusConnection(int id)
    {
        var serviceBusConnection = await _context.ServiceBusConnections.FindAsync(id);
        if (serviceBusConnection == null)
        {
            return NotFound();
        }

        _context.ServiceBusConnections.Remove(serviceBusConnection);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
