using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherBot
{
    public static class Weather
    {
        const string WeatherApiBaseUrl = "http://api.openweathermap.org/data/2.5/weather?{0}&units=imperial&APPID=a0f75f6b2f7ce29295822d2862df66a6";
        static HttpClient client = new HttpClient();

        public static async Task<string> GetWeatherByCityName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync(
                String.Format(WeatherApiBaseUrl, "q=" + cityName));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Uh oh, weather for {cityName} is not found";
            }
        }

        public static string GetCurrentTemperature(string cityName)
        {
            return string.Empty;
        }
    }
}
