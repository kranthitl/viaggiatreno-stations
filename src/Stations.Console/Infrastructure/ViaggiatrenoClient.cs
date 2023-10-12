using System.Net.Http.Json;
using Stations.Console.Dto;

namespace Stations.Console.Infrastructure;


public interface IViaggiatrenoClient
{
    public Task<StationByRegionDto[]> StationsByRegionId(int regionId);
}
public class ViaggiatrenoClient : IViaggiatrenoClient
{
    private readonly HttpClient _httpClient;
    
    public ViaggiatrenoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<StationByRegionDto[]> StationsByRegionId(int regionId)
    {
        var response = await _httpClient.GetAsync($"elencoStazioni/{regionId}");

        var stations = await response.Content.ReadFromJsonAsync<StationByRegionDto[]>();

        return stations;
    }
}