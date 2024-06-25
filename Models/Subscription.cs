namespace RevenueRecognitionSystem.models;

public class Subscription
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MonthlyCost { get; set; }
    public bool IsActive { get; set; }
}