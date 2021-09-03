
using System.Text.Json;
using WeazorApi.Controllers;

namespace WeazorApi;
public class LocationService : ILocationService
{
    public List<string> CountryList { get; } = new List<string>();
    public Dictionary<string, List<string>> CitiesListByCountryDictionary { get; } = new Dictionary<string, List<string>>();

    public LocationService()
    {
        using FileStream openStream = File.OpenRead("Assets/CountryCity.json");
        var countryCityDocument = JsonSerializer.Deserialize<CountryCityDocument[]>(openStream);

        foreach (var countryCity in countryCityDocument)
        {
            CountryList.Add(countryCity.country);
            List<string> cities = new List<string>();
            foreach (var city in countryCity.cities)
            {
                cities.Add(city);
            }

            CitiesListByCountryDictionary.Add(countryCity.country, cities);
        }
    }
}
