using Skender.Stock.Indicators;

public class TimeSerieItem
{
    public static List<string> Instruments { get; set; }
    public string Symbol { get; set; }
    public string Interval { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public List<decimal?> Tenkan { get; set; }
    public List<decimal?> Kijun { get; set; }
    public List<decimal?> SenkouA { get; set; }
    public List<decimal?> SenkouB { get; set; }
    public List<decimal?> Chikou { get; set; }
    public List<string?> Indicator { get; set; }


    public override bool Equals(object obj)
    {
        var item = obj as TimeSerieItem;
        if (item == null)
        {
            return false;
        }

        return this.Symbol == item.Symbol && this.Interval == item.Interval && this.Date == item.Date;
    }

    public override int GetHashCode()
    {
        return this.Symbol.GetHashCode() ^ this.Interval.GetHashCode() ^ this.Date.GetHashCode();
    }

    public override string ToString()
    {
        string tenkan = "";
        string kijun = "";
        string senkouA = "";
        string senkouB = "";
        string chikou = "";
        string indicator = "";
        if (Tenkan.Count > 0)
        {
            tenkan = Tenkan[Tenkan.Count - 1].ToString();
        }

        if (Kijun.Count > 0)
        {
            kijun = Kijun[Kijun.Count - 1].ToString();
        }

        if (SenkouA.Count > 0)
        {
            senkouA = SenkouA[SenkouA.Count - 1].ToString();
        }

        if (SenkouB.Count > 0)
        {
            senkouB = SenkouB[SenkouB.Count - 1].ToString();
        }

        if (Chikou.Count > 0)
        {
            chikou = Chikou[Chikou.Count - 1].ToString();
        }

        if (Indicator.Count > 0)
        {
            indicator = Indicator[Indicator.Count - 1].ToString();
        }

        return
            $"{Symbol?.PadRight(10)} | {Interval} | {indicator?.PadRight(10)}  | Price {Price.ToString()?.PadLeft(15)} | Date {Date}, Indicator ConversionLine: {tenkan}, BaseLine: {kijun}, LeadingSpanA: {senkouA}, LeadingSpanB: {senkouB}, LaggingLine: {chikou}";
    }
}