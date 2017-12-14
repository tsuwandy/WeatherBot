using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherBot
{
    public static class Weather
    {
        const string WeatherApiBaseUrl = "http://api.openweathermap.org/data/2.5/{0}?{1}={2}&units=imperial&APPID=a0f75f6b2f7ce29295822d2862df66a6";
        const string CurrentWeather = "weather";
        const string Forecast = "forecast";
        const string CityNameParam = "q";

        static HttpClient client = new HttpClient();

        public static async Task<string> GetCurrentWeatherByCityName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync(
                String.Format(WeatherApiBaseUrl, CurrentWeather, CityNameParam, cityName));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"Uh oh, weather for {cityName} is not found";
            }
        }

        public static async Task<string> GetWeatherForecastByCityName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync(
                String.Format(WeatherApiBaseUrl, Forecast, CityNameParam, cityName));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
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
            int idx = text.IndexOf("for");
            int len = 4;
            if (idx == 0)
            {
                idx = text.IndexOf("in");
                if (idx > 0)
                {
                    len = 3;
                }
            }

            if (idx > 0)
            {
                return text.Substring(idx + len);
            }

            return String.Empty;
        }
    }
}
