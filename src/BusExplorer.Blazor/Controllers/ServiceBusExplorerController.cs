using BusExplorer.Blazor.Models;
using BusExplorer.Blazor.Models;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.AspNetCore.Mvc;

namespace BusExplorer.Blazor.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceBusExplorerController : ControllerBase
{
    [HttpGet("topics")]
    public async Task<ActionResult<IEnumerable<TopicInfo>>> GetTopics([FromQuery] string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            return BadRequest("Connection string is required.");
        }

        try
        {
            var client = new ServiceBusAdministrationClient(connectionString);
            
            var topics = new List<TopicInfo>();
            await foreach (var topic in client.GetTopicsAsync())
            {
                var topicInfo = new TopicInfo { Name = topic.Name };
                await foreach (var subscription in client.GetSubscriptionsAsync(topic.Name))
                {
                    var runtimeProperties = (await client.GetSubscriptionRuntimePropertiesAsync(topic.Name, subscription.SubscriptionName)).Value;
                    topicInfo.Subscriptions.Add(new SubscriptionInfo
                    {
                        Name = subscription.SubscriptionName,
                        MessageCount = runtimeProperties.ActiveMessageCount,
                        DeadLetterMessageCount = runtimeProperties.DeadLetterMessageCount
                    });
                }
                topics.Add(topicInfo);
            }
            return Ok(topics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error getting topics: {ex.Message}");
        }
    }
}

