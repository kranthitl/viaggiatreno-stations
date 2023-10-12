using System.Globalization;
using System.Reflection;
using CsvHelper;
using Stations.Console.Dto;

namespace Stations.Console;

public interface IStationsProvider
{
    public string Save(StationDto[] stations);
}

public class StationsProvider : IStationsProvider
{
    public string Save(StationDto[] stations)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var filePath = Path.Combine(location, $"stations-{DateTime.UtcNow.Ticks}.csv");
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(stations);

        return filePath;
    }
}