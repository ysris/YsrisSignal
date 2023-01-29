using System.Net;
using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;

namespace YsrisSignal;

public class YsrisStrategy
{
    public YsrisStrategy(params string[] args)
    {
        Instruments = args.Select(symbol => new Instrument(symbol)).ToList();
    }

    public static List<Instrument> Instruments { get; set; }

    /// <summary>
    /// Run strategy
    /// </summary>
    public List<Instrument> Run()
    {
        return Instruments.Select(Run).ToList();
    }

    /// <summary>
    /// Run strategy for an instrument
    /// </summary>
    public Instrument Run(Instrument instrument)
    {
        instrument.FourHourSignal = Run(instrument, "4h");
        instrument.OneHourSignal = Run(instrument, "1h");
        return instrument;
    }

    /// <summary>
    /// Run strategy for an instrument interval
    /// </summary>
    public List<TimeSerieItem> Run(Instrument instrument, string interval)
    {
        var ts = GetTimeSerie(instrument, interval);
        ts = GetIndicator(ts);
        return ts;
    }

    /// <summary>
    /// Get OHLC time serie for instrument interval
    /// </summary>
    public List<TimeSerieItem> GetTimeSerie(Instrument instrument, string interval)
    {
        var client = new WebClient();
        var json = client.DownloadString("https://api.binance.com/api/v3/klines?symbol=" + instrument.Symbol + "&interval=" + interval);
        var prices = JArray.Parse(json);
        var ts = prices.Select(a => GetTimeSerieItem(a, interval)).ToList();
        return ts;
    }

    /// <summary>
    /// Parse time serie item
    /// </summary>
    public TimeSerieItem GetTimeSerieItem(JToken price, string interval)
    {
        return new TimeSerieItem
        {
            Open = price[1].Value<decimal>(),
            High = price[2].Value<decimal>(),
            Low = price[3].Value<decimal>(),
            Close = price[4].Value<decimal>(),
            Date = DateTimeOffset.FromUnixTimeMilliseconds(price[6].Value<long>()).UtcDateTime.AddHours(
                interval == "1h" ? -1 :
                interval == "4h" ? -4 :
                throw new NotImplementedException()
            ).AddSeconds(1)
        };
    }

    /// <summary>
    /// Get indicators for a time serie
    /// </summary>
    private List<TimeSerieItem> GetIndicator(List<TimeSerieItem> ts)
    {
        var indicatorTs = ts.GetIchimoku();
        ts = ts.Zip(
            indicatorTs,
            (timeSerieItem, resultitem) => timeSerieItem
                .SetFrom(resultitem)
                .RunIndicator()
        ).ToList();
        return ts;
    }
}