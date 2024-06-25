namespace RevenueRecognitionSystem.DTOs;

public class SoftwareDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CurrentVersion { get; set; }
    public string Category { get; set; }
    public decimal UpfrontCost { get; set; }
    public decimal SubscriptionCost { get; set; }
}