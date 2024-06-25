namespace RevenueRecognitionSystem.DTOs;

public class ContractDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public int SupportYears { get; set; }
    public bool IsPaid { get; set; }
    public bool IsCancelled { get; set; }
}