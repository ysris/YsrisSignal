public class Program
{
    public static void Main(string[] args)
    {
        TimeSerieItem.Instruments = new List<string> { "BTCUSDT", "XMRUSDT", "DOGEUSDT", "ETHUSDT", "LTCUSDT" };
        var binanceService = new BinanceService();

        while (true)
        {
            var timeSerieItems = binanceService.GetPrices();
            foreach (var timeSerie in timeSerieItems)
            {
                Console.WriteLine(timeSerie);
                //TimeSerieItemDal.Store(timeSerie);
            }

            Thread.Sleep(3600000);
        }
    }
}