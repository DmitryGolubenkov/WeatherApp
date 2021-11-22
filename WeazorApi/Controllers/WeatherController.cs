using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeazorApi.Controllers;
[ApiController]
[Route("api/")]
public class WeatherController : ControllerBase
{

    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;


    public WeatherController(IConfiguration config, IMemoryCache cache)
    {
        _config = config;
        _cache = cache;
    }

    /// <summary>
    /// Gets OpenWeatherApi key from appsettings.json.
    /// </summary>
    public string ApiKey => _config.GetValue<string>("OpenWeatherApiKey");

    /// <summary>
    /// API route. Returns data about current weather. Uses caching.
    /// </summary>
    /// <param name="cityName">City name.</param>
    /// <param name="countryName">Country name.</param>
    /// <returns>JSON string containing data about current weather.</returns>
    [HttpGet]
    [Route("GetCurrentWeather")]
    public async Task<string> GetCurrentWeatherAsync(string cityName, string countryName)
    {
        string cityString = cityName + ',' + countryName;
        string cacheKey = cityString + "-GetCurrentWeather";
        //Check cache
        if (!_cache.TryGetValue(cacheKey, out string currentWeatherJson))
        {
            //If no cached data is found (or it is too old), make a call to API.
            StringBuilder apiCallBuilder = new StringBuilder();
            apiCallBuilder.Append("https://api.openweathermap.org/data/2.5/weather?q=").Append(cityString).Append("&appid=").Append(ApiKey);


            using HttpClient client = new HttpClient();
            currentWeatherJson = await client.GetStringAsync(apiCallBuilder.ToString());

            //Cache new data
            _cache.Set(cacheKey, currentWeatherJson, new DateTimeOffset(DateTime.Now.AddHours(1)));
        }
        return currentWeatherJson;
    }

    /// <summary>
    /// API route. Returns 5 day forecast. Uses caching.
    /// </summary>
    /// <param name="cityName">City name.</param>
    /// <param name="countryName">Country name.</param>
    /// <returns>JSON string containing data about current weather.</returns>
    [HttpGet]
    [Route("Get5DaysForecast")]
    public async Task<string> Get5DaysForecastAsync(string cityName, string countryName)
    {
        string cityString = cityName + ',' + countryName;
        string cacheKey = cityString + "-Get5DaysForecast";
        if (!_cache.TryGetValue(cacheKey, out string forecast))
        {
            StringBuilder apiCall = new StringBuilder();
            apiCall.Append("https://api.openweathermap.org/data/2.5/forecast?q=").Append(cityString).Append("&appid=").Append(ApiKey);

            using HttpClient client = new HttpClient();
            forecast = await client.GetStringAsync(apiCall.ToString());
            _cache.Set(cacheKey, forecast, new DateTimeOffset(DateTime.Now.AddHours(1)));
        }
        return forecast;
    }
}
