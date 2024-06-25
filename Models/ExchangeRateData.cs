namespace RevenueRecognitionSystem.models;

public class ExchangeRateData
{
    public string Base { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
}