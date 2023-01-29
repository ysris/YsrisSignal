namespace YsrisSignal;

public static class Program
{
    public static void Main(string[] args)
    {
        var strategy = new YsrisStrategy("BTCUSDT", "XMRUSDT", "DOGEUSDT", "ETHUSDT", "LTCUSDT");
        while (true)
        {
            strategy.Run().ForEach(Console.WriteLine);
            Thread.Sleep(3600000);
        }
    }
}