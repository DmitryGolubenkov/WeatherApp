using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WeazorLib
{
    public static class LocationService
    {
        private static string[] _ipAddressApis = new string[]
        {
            "https://ipinfo.io/ip/",
            "https://api.ipify.org/",
            "https://icanhazip.com/",
            "https://checkip.amazonaws.com/",
            "https://wtfismyip.com/text"
        };

        /// <summary>
        /// Method is used to get current public IP address.
        /// </summary>
        /// <returns>Public IP address of user. Throws HttpRequestException if there is no internet connection.</returns>
        public static async Task<string> GetIpAddress()
        {
            //Try to get and validate IP address from different services.
            string ip;
            using (var client = new HttpClient())
            {

                foreach (var ipAddressServiceDomain in _ipAddressApis)
                {
                    ip = await client.GetStringAsync(ipAddressServiceDomain);
                    if (IPAddress.TryParse(ip, out _))
                        return ip;
                }
            }
            //If the response is still empty, then it is possible that there is no internet connection.
            throw new HttpRequestException();
        }
        /// <summary>
        /// Method makes a web request and returns data about city the ip address belongs to, then it searches city.list.json to find city ID in OpenWeatherMap.
        /// </summary>
        /// <param name="ipAddress">User IP Address.</param>
        /// <returns>OpenWeatherMap city ID.</returns>
        [Obsolete]
        public static async Task<UserLocation> GetLocationIdUsingIpAddress(string ipAddress)
        {
            StringBuilder sb = new StringBuilder(64);
            sb.Append("http://ip-api.com/json/").Append(ipAddress).Append("?fields=49366");
            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync(sb.ToString());
                var locationResponse = JsonSerializer.Deserialize<UserLocation>(response,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return locationResponse;
            };

            

        }

        public static async Task<UserLocation> GetLocationInfoAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetFromJsonAsync<UserLocation>("https://ipapi.co/json");

                if (response is not null)
                    return response;
                else
                    throw new HttpRequestException();
            }
        }
    }
}
