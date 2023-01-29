using Skender.Stock.Indicators;

namespace YsrisSignal;

public class TimeSerieItem : Quote
{
    public string Symbol { get; set; }
    public string Interval { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public decimal? Tenkan { get; set; }
    public decimal? Kijun { get; set; }
    public decimal? SenkouA { get; set; }
    public decimal? SenkouB { get; set; }
    public decimal? Chikou { get; set; }
    public string? Indicator { get; set; }


    public override bool Equals(object obj)
    {
        var item = obj as TimeSerieItem;
        if (item == null)
        {
            return false;
        }

        return Symbol == item.Symbol && Interval == item.Interval && Date == item.Date;
    }

    public override int GetHashCode()
    {
        return Symbol.GetHashCode() ^ Interval.GetHashCode() ^ Date.GetHashCode();
    }

    public override string ToString()
    {
        return
            $"{Symbol?.PadRight(10)} | {Interval} | {Indicator?.PadRight(10)}  | Price {Price.ToString()?.PadLeft(15)} | Date {Date}, Indicator ConversionLine: {Tenkan}, BaseLine: {Kijun}, LeadingSpanA: {SenkouA}, LeadingSpanB: {SenkouB}, LaggingLine: {Chikou}";
    }

    public TimeSerieItem SetFrom(IchimokuResult resultitem)
    {
        Tenkan = resultitem.TenkanSen;
        Kijun = resultitem.KijunSen;
        SenkouA = resultitem.SenkouSpanA;
        SenkouB = resultitem.SenkouSpanB;
        Chikou = resultitem.ChikouSpan;
        return this;
    }

    public TimeSerieItem RunIndicator()
    {
        if (SenkouA == null || Tenkan == null || Kijun == null)
            return this;

        var indicatorValue = Close - SenkouA.Value;
        var kijunTenkan = Kijun.Value - Tenkan.Value;
        if (indicatorValue > 0 && kijunTenkan > 0)
            Indicator = "Buy";
        else if (indicatorValue < 0 && kijunTenkan < 0)
            Indicator = "Sell";
        else
            Indicator = "Neutral";
        return this;
    }
}