namespace WeazorApi
{
    public interface ILocationService
    {
        Dictionary<string, List<string>> CitiesListByCountryDictionary { get; }
        List<string> CountryList { get; }
    }
}