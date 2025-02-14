namespace StockMaster.Models.CoPhieu69;

public record Company
{
    public int Id { get; set; }
    public string TickerName { get; init; }
    public string FullName { get; init; }
    public double Price { get; init; }
    public double Changes { get; init; }
    public double VolumeTwentyFourHour { get; init; }
    public double VolumeFiftyTwoWeek { get; init; }
    public double VolumeRegistered { get; init; }
    public double MarketCap { get; init; }
    public int PercentageOfForeignOwner { get; init; }
    public string Chart { get; init; }
}