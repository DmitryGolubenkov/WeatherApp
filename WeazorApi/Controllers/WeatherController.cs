using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text;
using System.Text.Json;
using WeazorLib;

namespace WeazorApi.Controllers;
[ApiController]
public class WeatherController : ControllerBase
{

    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    public WeatherController(IConfiguration config, IMemoryCache cache)
    {
        _config = config;
        _cache = cache;
    }

    public string ApiKey => _config.GetValue<string>("OpenWeatherApiKey");


    /*[HttpGet]
    [Route("api/GetCityIdFromUserLocation")]
    public async Task<int> GetCityIdFromUserLocation(UserLocation location)
    {
        //TODO: add failsaves
        await using (FileStream fileStream = new FileStream(_webHostEnvironment.ContentRootPath + "/AppData/city.list.json", FileMode.Open))
        {
            var locations = JsonSerializer.Deserialize<List<OpenWeatherMapLocation>>(fileStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return locations.Find(
                x => x.Name == location.City
                && x.Country == location.CountryCode
                //TODO: сравнить широту и долготу (только целые)
                ).Id;
        }
    }
    */

    [HttpGet]
    [Route("api/GetForecast")]
    public async Task<string> GetForecastAsync(string cityName, string countryName)
    {
        string forecast;
        string cityString = cityName + ',' + countryName;
        //Check cache
        if (!_cache.TryGetValue(cityString, out forecast))
        {
            //If no cached data is found (or it is too old), make a call to API.
            StringBuilder apiCallBuilder = new StringBuilder();
            apiCallBuilder.Append("https://api.openweathermap.org/data/2.5/weather?q=").Append(cityString).Append("&appid=").Append(ApiKey);


            using HttpClient client = new HttpClient();
            forecast = await client.GetStringAsync(apiCallBuilder.ToString());

            //Cache new data
            _cache.Set(cityString, forecast, new DateTimeOffset(DateTime.Now.AddHours(1)));
        }
        return forecast;
    }
}
