namespace RevenueRecognitionSystem.models;

public class Contract
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }
    public int SupportYears { get; set; } = 1;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}