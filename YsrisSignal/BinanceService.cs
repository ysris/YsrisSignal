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
        return TimeSerieItem.Instruments
            .SelectMany(instrument => new List<TimeSerieItem>
            {
                GetPrice(instrument, "1h"),
                GetPrice(instrument, "4h")
            })
            .ToList();
    }

    public TimeSerieItem GetPrice(string symbol, string interval)
    {
        var priceVisitor = new GetPriceVisitor { Symbol = symbol, Interval = interval };
        Accept(priceVisitor);
        var ichimokuVisitor = new IchimokuVisitor();
        Accept(ichimokuVisitor);
        var timeSerieItem = new TimeSerieItem
        {
            Symbol = symbol,
            Interval = interval,
            Price = priceVisitor.Price,
            Date = priceVisitor.Date,
            Tenkan = ichimokuVisitor.Tenkan,
            Kijun = ichimokuVisitor.Kijun,
            SenkouA = ichimokuVisitor.SenkouA,
            SenkouB = ichimokuVisitor.SenkouB,
            Chikou = ichimokuVisitor.Chikou,
            Indicator = ichimokuVisitor.Indicator,
        };
        return timeSerieItem;
    }

    public List<Quote> TimeSerie { get; set; }
}