namespace BusExplorer.Blazor.Models;

public class SubscriptionInfo
{
    public string Name { get; set; }
    public long MessageCount { get; set; }
    public long DeadLetterMessageCount { get; set; }
}

public class TopicInfo
{
    public string Name { get; set; }
    public List<SubscriptionInfo> Subscriptions { get; set; } = new List<SubscriptionInfo>();
}