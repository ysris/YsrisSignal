using Skender.Stock.Indicators;

public class IchimokuVisitor : IVisitor
{
    public List<decimal?> Tenkan { get; set; }
    public List<decimal?> Kijun { get; set; }
    public List<decimal?> SenkouA { get; set; }
    public List<decimal?> SenkouB { get; set; }
    public List<decimal?> Chikou { get; set; }
    public List<string> Indicator { get; set; }

    public void Visit(BinanceService service)
    {
        // Compute the Ichimoku indicator using the prices
        var results = Skender.Stock.Indicators.Indicator.GetIchimoku(service.TimeSerie).ToList();
        Tenkan = results.Select(x => x.TenkanSen).ToList();
        Kijun = results.Select(x => x.KijunSen).ToList();
        SenkouA = results.Select(x => x.SenkouSpanA).ToList();
        SenkouB = results.Select(x => x.SenkouSpanB).ToList();
        Chikou = results.Select(x => x.ChikouSpan).ToList();
        Indicator = new List<string>();
        for (int i = 0; i < results.Count(); i++)
        {
            Indicator.Add(GetIndicatorValue(service.TimeSerie[i], results[i]));
        }
    }

    private string GetIndicatorValue(Quote timeSerie, IchimokuResult ichimoku)
    {
        if (ichimoku.SenkouSpanA == null || ichimoku.TenkanSen == null || ichimoku.KijunSen == null)
            return string.Empty;

        var indicatorValue = timeSerie.Close - ichimoku.SenkouSpanA.Value;
        var kijunTenkan = ichimoku.KijunSen.Value - ichimoku.TenkanSen.Value;
        if (indicatorValue > 0 && kijunTenkan > 0)
            return "Buy";
        else if (indicatorValue < 0 && kijunTenkan < 0)
            return "Sell";
        else
            return "Neutral";
    }
}