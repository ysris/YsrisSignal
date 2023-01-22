/*
CREATE TABLE Instruments (
    Symbol varchar(10) NOT NULL PRIMARY KEY
);

INSERT INTO Instruments (Symbol) VALUES ('BTCUSDT'), ('XMRUSDT'), ('DOGEUSDT'), ('ETHUSDT'), ('LTCUSDT');

CREATE TABLE TimeSerieItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Symbol VARCHAR(10) NOT NULL,
    Interval VARCHAR(10) NOT NULL,
    Price DECIMAL(10,8) NOT NULL,
    Date DATETIME NOT NULL,
    Tenkan VARCHAR(MAX) NULL,
    Kijun VARCHAR(MAX) NULL,
    SenkouA VARCHAR(MAX) NULL,
    SenkouB VARCHAR(MAX) NULL,
    Chikou VARCHAR(MAX) NULL
);
*/

public class Program
{
    public static void Main(string[] args)
    {
        TimeSerieItem.Instruments = new List<string> { "BTCUSDT", "XMRUSDT", "DOGEUSDT", "ETHUSDT", "LTCUSDT" };
        var binanceService = new BinanceService();

        while (true)
        {
            var timeSerieItems = binanceService.GetPrices();
            foreach (TimeSerieItem timeSerie in timeSerieItems)
            {
                Console.WriteLine(timeSerie);
                // Save the TimeSerieItem to the database
                //TimeSerieItemDal.Store(timeSerie);
            }

            // Wait one hour before getting the prices again
            System.Threading.Thread.Sleep(3600000);
        }
    }
}