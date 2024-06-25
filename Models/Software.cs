namespace RevenueRecognitionSystem.models;

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CurrentVersion { get; set; }
    public string Category { get; set; }
    public decimal UpfrontCost { get; set; }
    public decimal SubscriptionCost { get; set; }
    public List<Discount> Discounts { get; set; } = new List<Discount>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}