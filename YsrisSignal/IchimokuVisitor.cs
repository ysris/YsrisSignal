using Skender.Stock.Indicators;

public class IchimokuVisitor : IVisitor
{
    public List<decimal?> Tenkan { get; set; }
    public List<decimal?> Kijun { get; set; }
    public List<decimal?> SenkouA { get; set; }
    public List<decimal?> SenkouB { get; set; }
    public List<decimal?> Chikou { get; set; }

    public void Visit(BinanceService service)
    {
        // Compute the Ichimoku indicator using the prices
        var results = Indicator.GetIchimoku(service.TimeSerie);
        Tenkan = results.Select(x => x.TenkanSen).ToList();
        Kijun = results.Select(x => x.KijunSen).ToList();
        SenkouA = results.Select(x => x.SenkouSpanA).ToList();
        SenkouB = results.Select(x => x.SenkouSpanB).ToList();
        Chikou = results.Select(x => x.ChikouSpan).ToList();
    }
}