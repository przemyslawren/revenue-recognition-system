namespace RevenueRecognitionSystem.models;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OfferType { get; set; }
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}