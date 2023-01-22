using System.Net;
using Newtonsoft.Json.Linq;
using Skender.Stock.Indicators;

public class GetPriceVisitor : IVisitor
{
    public string Symbol { get; set; }
    public string Interval { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }

    public void Visit(BinanceService service)
    {
        // Call Binance API to get the prices
        var client = new WebClient();
        var json = client.DownloadString("https://api.binance.com/api/v3/klines?symbol=" + Symbol + "&interval=" + Interval);

        // Deserialize the JSON data using Newtonsoft.Json
        var data = JArray.Parse(json);

        var prices = (JArray)data;
        var ts = new List<Quote>();
        for (int i = 0; i < prices.Count; i++)
        {
            ts.Add(new Quote
            {
                Open = prices[i][1].Value<decimal>(),
                High = prices[i][2].Value<decimal>(),
                Low = prices[i][3].Value<decimal>(),
                Close = prices[i][4].Value<decimal>(),
                Date = DateTimeOffset.FromUnixTimeMilliseconds(prices[i][6].Value<long>()).UtcDateTime.AddHours(
                    Interval == "1h" ? -1 :
                    Interval == "4h" ? -4 :
                    throw new NotImplementedException()
                ).AddSeconds(1)
            });
        }

        Price = ts.Last().Close;
        Date = ts.Last().Date;

        service.TimeSerie = ts;
    }
}