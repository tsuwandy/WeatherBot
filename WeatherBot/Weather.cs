using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherBot
{
    public static class Weather
    {
        const string CurrentConditionApiUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22{0}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        const string ForecastApiUrl = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22{0}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
        
        static string[] Cardinals = { "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N" };

        public static string DegreesToCardinal(double degrees)
        {
            return Cardinals[(int)Math.Round(((double)degrees % 360) / 45)];
        }

        public static async Task<string> GetCurrentWeatherByCityNameAsync(string cityName)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(String.Format(ForecastApiUrl, cityName));
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            else
            {
                return $"Uh oh, weather for {cityName} is not found";
            }
        }

        public static async Task<string> GetWeatherForecastByCityNameAsync(string cityName)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(String.Format(ForecastApiUrl, cityName));
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            else
            {
                return $"Uh oh, weather forecast for {cityName} is not found";
            }
        }

        public static string GetCurrentTemperature(string cityName)
        {
            return string.Empty;
        }

        public static string GetCity(string text)
        {
            int idx = text.IndexOf("for ");
            int len = 4;
            if (idx == -1)
            {
                idx = text.IndexOf("in ");
                if (idx > 0)
                {
                    len = 3;
                }
            }

            if (idx > 0)
            {
                string city = text.Substring(idx + len);
                return Char.ToUpperInvariant(city[0]) + city.Substring(1);
            }

            return String.Empty;
        }
    }
}
