namespace RevenueRecognitionSystem.models;

public abstract class Client
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}