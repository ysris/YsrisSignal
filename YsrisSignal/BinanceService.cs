using System.Net;
using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;

public class BinanceService
{

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public List<TimeSerieItem> GetPrices()
    {
        var timeSerieItems = new List<TimeSerieItem>();
        foreach (string instrument in TimeSerieItem.Instruments)
        {
            var timeSerie = GetPrice(instrument, "1h");
            timeSerieItems.Add(timeSerie);
        }

        return timeSerieItems;
    }

    public TimeSerieItem GetPrice(string symbol, string interval)
    {
        var priceVisitor = new GetPriceVisitor { Symbol = symbol, Interval = interval };
        Accept(priceVisitor);
        var ichimokuVisitor = new IchimokuVisitor();
        Accept(ichimokuVisitor);
        return new TimeSerieItem
        {
            Symbol = symbol,
            Interval = interval,
            Price = priceVisitor.Price,
            Date = priceVisitor.Date,
            Tenkan = ichimokuVisitor.Tenkan,
            Kijun = ichimokuVisitor.Kijun,
            SenkouA = ichimokuVisitor.SenkouA,
            SenkouB = ichimokuVisitor.SenkouB,
            Chikou = ichimokuVisitor.Chikou
        };
    }

    public List<Quote> TimeSerie { get; set; }
}