using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stations.Console;
using Stations.Console.Dto;
using Stations.Console.Infrastructure;

using var host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var client = scope.ServiceProvider.GetRequiredService<IViaggiatrenoClient>();

var regionProvider = scope.ServiceProvider.GetRequiredService<IRegionProvider>();

var stations = new List<StationDto>();

foreach (var region in regionProvider.Regions())
{
    var response = await client.StationsByRegionId(region.Id);
    stations.AddRange(response.Select(station => new StationDto(region.Id,region.Region,station.CodStazione,station.Localita.NomeBreve,station.Lat,station.Lon)));
}

var stationsProvider = scope.ServiceProvider.GetRequiredService<IStationsProvider>();

var filePath = stationsProvider.Save(stations.ToArray());

Console.WriteLine("finished writing stations to {0}",filePath);

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddHttpClient<IViaggiatrenoClient,ViaggiatrenoClient>(client =>
            {
                client.BaseAddress = new Uri("http://www.viaggiatreno.it/infomobilita/resteasy/viaggiatreno/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<IRegionProvider, RegionProvider>();
            services.AddScoped<IStationsProvider, StationsProvider>();
        });
}