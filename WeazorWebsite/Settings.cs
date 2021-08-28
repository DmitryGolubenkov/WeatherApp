
namespace WeazorWebsite;
public class Settings
{
    public string WeazorApiUrl { get; set; }

    public Settings(string weazorApiUrl)
    {
        WeazorApiUrl = weazorApiUrl;
    }
}
