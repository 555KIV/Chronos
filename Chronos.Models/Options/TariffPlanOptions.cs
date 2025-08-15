namespace Chronos.Models.Options;

public class TariffPlanOptions
{
    public decimal TrafficVolume { get; set; }
    public decimal PriceTariffPerDay { get; set; }
    public decimal PricePerExcess { get; set; }
}