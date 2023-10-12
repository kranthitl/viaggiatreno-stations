using System.Globalization;
using System.Reflection;
using CsvHelper;
using Stations.Console.Dto;

namespace Stations.Console;

public interface IRegionProvider
{
    public RegionDto[] Regions();
}

public class RegionProvider : IRegionProvider
{
    public RegionDto[] Regions()
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "regions.csv");
        using var reader = new StreamReader(path);
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var regions = csv.GetRecords<RegionDto>().ToArray();
        return regions;
    }
}