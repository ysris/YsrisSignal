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

        return
            $"Symbol: {Symbol}, Interval: {Interval}, Price: {Price}, Date: {Date}, ConversionLine: {tenkan}, BaseLine: {kijun}, LeadingSpanA: {senkouA}, LeadingSpanB: {senkouB}, LaggingLine: {chikou}";
    }
}