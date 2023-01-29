using System.Net;
using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;

namespace YsrisSignal;

public class YsrisStrategy
{
    public YsrisStrategy(params string[] args)
    {
        Instruments = args.ToList();
    }

    public static List<string> Instruments { get; set; }

    public List<YsrisSignal.Instrument> Run()
    {
        return Instruments.Select(GetForInstrument).ToList();
    }

    public YsrisSignal.Instrument GetForInstrument(string symbol)
    {
        return new Instrument
        {
            Symbol = symbol,
            FourHourSignal = GetTimeSerie(symbol, "4h"),
            OneHourSignal = GetTimeSerie(symbol, "1h"),
        };
    }

    public List<TimeSerieItem> GetTimeSerie(string symbol, string interval)
    {
        // Get time serie

        var client = new WebClient();
        var json = client.DownloadString("https://api.binance.com/api/v3/klines?symbol=" + symbol + "&interval=" + interval);
        var prices = JArray.Parse(json);

        var ts = prices.Select(a => GetTimeSerieItem(a, interval)).ToList();
        ts = GetIchimoku(ts);
        ts = GetIndicator(ts);

        return ts;
    }

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

    private List<TimeSerieItem> GetIchimoku(List<TimeSerieItem> ts)
    {
        var indicatorTs = Indicator.GetIchimoku(ts);
        return ts.Zip(indicatorTs, GetIchimokuItem).ToList();
    }

    private TimeSerieItem GetIchimokuItem(TimeSerieItem tsitemt, IchimokuResult resultitem)
    {
        tsitemt.Tenkan = resultitem.TenkanSen;
        tsitemt.Kijun = resultitem.KijunSen;
        tsitemt.SenkouA = resultitem.SenkouSpanA;
        tsitemt.SenkouB = resultitem.SenkouSpanB;
        tsitemt.Chikou = resultitem.ChikouSpan;
        return tsitemt;
    }

    private List<TimeSerieItem> GetIndicator(List<TimeSerieItem> ts)
    {
        ts = ts.Select(GetIndicatorItem).ToList();
        return ts;
    }

    private TimeSerieItem GetIndicatorItem(TimeSerieItem ichimoku)
    {
        if (ichimoku.SenkouA == null || ichimoku.Tenkan == null || ichimoku.Kijun == null)
            return ichimoku;

        var indicatorValue = ichimoku.Close - ichimoku.SenkouA.Value;
        var kijunTenkan = ichimoku.Kijun.Value - ichimoku.Tenkan.Value;
        if (indicatorValue > 0 && kijunTenkan > 0)
            ichimoku.Indicator = "Buy";
        else if (indicatorValue < 0 && kijunTenkan < 0)
            ichimoku.Indicator = "Sell";
        else
            ichimoku.Indicator = "Neutral";

        return ichimoku;
    }
}