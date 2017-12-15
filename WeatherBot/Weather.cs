using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherBot
{
    public static class Weather
    {
        const string WeatherApiBaseUrl = "http://api.openweathermap.org/data/2.5/{0}?q={1}&units=imperial&APPID=a0f75f6b2f7ce29295822d2862df66a6{2}";
        const string CurrentWeather = "weather";
        const string Forecast = "forecast/daily";
        const string CityNameParam = "q";
        const string NumForecastDaysParam = "&cnt=3";
        
        static HttpClient client = new HttpClient();

        public static async Task<string> GetCurrentWeatherByCityName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync(
                String.Format(WeatherApiBaseUrl, CurrentWeather, cityName, String.Empty));
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

        public static async Task<string> GetWeatherForecastByCityName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync(
                String.Format(WeatherApiBaseUrl, Forecast, cityName, NumForecastDaysParam));
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
