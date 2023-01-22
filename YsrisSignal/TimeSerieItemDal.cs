using System.Data.SqlClient;
using Dapper;

public class TimeSerieItemDal
{
    private static string connectionString = "Data Source=(local);Initial Catalog=YsrisSignal;Integrated Security=True;";

    public static void Store(TimeSerieItem item)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Execute(
                "INSERT INTO TimeSerieItem (Symbol, Interval, Price, Date, Tenkan, Kijun, SenkouA, SenkouB, Chikou) " +
                "VALUES (@Symbol, @Interval, @Price, @Date, @Tenkan, @Kijun, @SenkouA, @SenkouB, @Chikou)",
                new
                {
                    Symbol = item.Symbol,
                    Interval = item.Interval,
                    Price = item.Price,
                    Date = item.Date,
                    Tenkan = Newtonsoft.Json.JsonConvert.SerializeObject(item.Tenkan),
                    Kijun = Newtonsoft.Json.JsonConvert.SerializeObject(item.Kijun),
                    SenkouA = Newtonsoft.Json.JsonConvert.SerializeObject(item.SenkouA),
                    SenkouB = Newtonsoft.Json.JsonConvert.SerializeObject(item.SenkouB),
                    Chikou = Newtonsoft.Json.JsonConvert.SerializeObject(item.Chikou)
                });
        }
    }
}