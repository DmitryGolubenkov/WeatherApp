using Microsoft.AspNetCore.Mvc;

namespace WeazorApi.Controllers;
[ApiController]
[Route("api/")]
public class LocationController : Controller
{
    private readonly ILocationService _locationService;
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    /// <summary>
    /// API route that returns a list of avaliable countries.
    /// </summary>
    /// <returns>A list of avaliable countries.</returns>
    [Route("GetCountriesList")]
    public List<string> GetCountriesList()
    {
        return _locationService.CountryList;
    }

    /// <summary>
    /// API route that returns a list of avaliable cities in the country.
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    [Route("GetCitiesList")]
    public List<string> GetCitiesList(string country)
    {
        return _locationService.CitiesListByCountryDictionary[country];
    }

}
