using Newtonsoft.Json;

namespace YsrisSignal;

public class Instrument
{
    public Instrument(string symbol)
    {
        Symbol = symbol;
    }

    public string Symbol { get; set; }
    public List<TimeSerieItem> FourHourSignal { get; set; }
    public List<TimeSerieItem> OneHourSignal { get; set; }
    public DateTime? GenerationDate4HR => FourHourSignal.Last().Date;
    public DateTime? GenerationDate1HR => OneHourSignal.Last().Date;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(new
        {
            Symbol,
            GenerationDate1HR,
            GenerationDate4HR,
            FourHour = FourHourSignal.Last().Indicator,
            OneHour = OneHourSignal.Last().Indicator,
        });
    }
}