using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Templates;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherBot
{
    public class WeatherView : TemplateRendererMiddleware
    {
        // template ids
        public const string CURRENT = "Weather.Current";
        public const string FORECAST = "Weather.Forecast";
        
        public static TemplateDictionary Templates = new TemplateDictionary
        {
            // Default templates
            ["default"] = new TemplateIdMap
                {
                    { CURRENT, (context, city) =>  GetWeatherCard(context, city, false) },
                    { FORECAST, (context, city) => GetWeatherCard(context, city, true) },
                }
        };

        const string TemplateCurrent = @"{
    ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
    ""type"": ""AdaptiveCard"",
    ""version"": ""1.0"",
    ""body"": [
        {
            ""type"": ""ColumnSet"",
            ""columns"": [
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""width"": ""small"",
                            ""text"": ""Tue 14"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""width"": ""small"",
                            ""text"": ""Seattle, WA"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                }
            ]
        },
        {
            ""type"": ""ColumnSet"",
            ""columns"": [
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""type"": ""Image"",
                            ""url"": ""https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Rain.png""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""size"": ""extraLarge"",
                            ""text"": ""49°"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""isSubtle"": true,
                            ""text"": ""43°"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                }
            ]
        },
        {
            ""text"": ""Rain shower"",
            ""type"": ""TextBlock"",
            ""weight"": ""bolder""
        },
        {
            ""isSubtle"": true,
            ""width"": ""small"",
            ""text"": ""Winds 5 mph NE"",
            ""type"": ""TextBlock""
        },
        {
            ""isSubtle"": true,
            ""width"": ""small"",
            ""text"": ""Updated 2:15 PM"",
            ""type"": ""TextBlock""
        }
    ]
}";

        const string TemplateForecast = @"{
    ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
    ""type"": ""AdaptiveCard"",
    ""version"": ""1.0"",
    ""body"": [
        {
            ""type"": ""ColumnSet"",
            ""columns"": [
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""width"": ""small"",
                            ""text"": ""Tue 14"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""width"": ""small"",
                            ""text"": ""Seattle, WA"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                }
            ]
        },
        {
            ""type"": ""ColumnSet"",
            ""columns"": [
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""type"": ""Image"",
                            ""url"": ""https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Rain.png""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""size"": ""extraLarge"",
                            ""text"": ""49°"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                },
                {
                    ""width"": ""auto"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""isSubtle"": true,
                            ""text"": ""43°"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        }
                    ]
                }
            ]
        },
        {
            ""text"": ""Rain shower"",
            ""type"": ""TextBlock"",
            ""weight"": ""bolder""
        },
        {
            ""isSubtle"": true,
            ""width"": ""small"",
            ""text"": ""Winds 5 mph NE"",
            ""type"": ""TextBlock""
        },
        {
            ""type"": ""ColumnSet"",
            ""columns"": [
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""size"": ""small"",
                            ""text"": ""Wed 15"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        },
                        {
                            ""size"": ""small"",
                            ""type"": ""Image"",
                            ""url"": ""https://raw.githubusercontent.com/tsuwandy/weather/master/rain_color.png""
                        },
                        {
                            ""size"": ""small"",
                            ""text"": ""47°"",
                            ""type"": ""TextBlock""
                        },
                        {
                            ""isSubtle"": true,
                            ""spacing"": ""none"",
                            ""size"": ""small"",
                            ""text"": ""40°"",
                            ""type"": ""TextBlock""
                        }
                    ]
                },
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""size"": ""small"",
                            ""text"": ""Thu 16"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        },
                        {
                            ""size"": ""small"",
                            ""type"": ""Image"",
                            ""url"": ""https://raw.githubusercontent.com/tsuwandy/weather/master/rain_color.png""
                        },
                        {
                            ""size"": ""small"",
                            ""text"": ""45°"",
                            ""type"": ""TextBlock""
                        },
                        {
                            ""isSubtle"": true,
                            ""spacing"": ""none"",
                            ""size"": ""small"",
                            ""text"": ""39°"",
                            ""type"": ""TextBlock""
                        }
                    ]
                },
                {
                    ""width"": ""stretch"",
                    ""type"": ""Column"",
                    ""items"": [
                        {
                            ""size"": ""small"",
                            ""text"": ""Fri 17"",
                            ""type"": ""TextBlock"",
                            ""weight"": ""bolder""
                        },
                        {
                            ""size"": ""small"",
                            ""type"": ""Image"",
                            ""url"": ""https://raw.githubusercontent.com/tsuwandy/weather/master/rain_color.png""
                        },
                        {
                            ""size"": ""small"",
                            ""text"": ""46°"",
                            ""type"": ""TextBlock""
                        },
                        {
                            ""isSubtle"": true,
                            ""spacing"": ""none"",
                            ""size"": ""small"",
                            ""text"": ""41°"",
                            ""type"": ""TextBlock""
                        }
                    ]
                }
            ]
        },
        {
            ""isSubtle"": true,
            ""width"": ""small"",
            ""text"": ""Updated 2:15 PM"",
            ""type"": ""TextBlock""
        }
    ]
}";

        static readonly Dictionary<string, string> LargeWeatherImages = new Dictionary<string, string>()
        {
            { "Cloudy", "https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Cloudy.png" },
            { "CloudyWithRain", "https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Cloudy_w_Rain.png" },
            { "Rain", "https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Rain.png" },
            { "Sunny", "https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Sunny.png" },
            { "Snow", "https://raw.githubusercontent.com/tsuwandy/weather/master/Weather-Snow.png" }
        };

        static readonly Dictionary<string, string> SmallWeatherImages = new Dictionary<string, string>()
        {
            { "Cloudy", "https://raw.githubusercontent.com/tsuwandy/weather/master/cloudy.png" },
            { "CloudyWithRain", "https://raw.githubusercontent.com/tsuwandy/weather/master/cloudy_w_rain.png" },
            { "Rain", "https://raw.githubusercontent.com/tsuwandy/weather/master/rain_color.png" },
            { "Sunny", "https://raw.githubusercontent.com/tsuwandy/weather/master/sunny.png" },
            { "Snow", "https://raw.githubusercontent.com/tsuwandy/weather/master/snow_color.png" }
        };

        public WeatherView() : base(new DictionaryRenderer(Templates))
        {
        }

        public static string GetWeatherImage(bool useSmallImage, string weatherText)
        {
            Dictionary<string, string> lookup = useSmallImage ? SmallWeatherImages : LargeWeatherImages;
            weatherText = weatherText.ToLowerInvariant();
            if (weatherText.Contains("cloudy"))
            {
                return lookup["Cloudy"];
            }
            else if (weatherText.Contains("rain") || 
                weatherText.Contains("showers") || 
                weatherText.Contains("thunderstorms"))
            {
                return lookup["Rain"];
            }
            else if (weatherText.Contains("snow"))
            {
                return lookup["Snow"];
            }
            else
            {
                return lookup["Sunny"];
            }
        }

        public static IMessageActivity GetWeatherCard(BotContext context, string city, bool isForecast)
        {
            return Task.Run(() => GetWeatherCardAsync(context, city, isForecast)).Result;
        }

        public static async Task<IMessageActivity> GetWeatherCardAsync(BotContext context, string city, bool isForecast)
        {
            IMessageActivity activity = context.Request.CreateReply();
            AdaptiveCard card = JsonConvert.DeserializeObject<AdaptiveCard>(isForecast ? TemplateForecast : TemplateCurrent);
            DateTime now = DateTime.Now;
            string weatherJson = await Weather.GetWeatherForecastByCityNameAsync(city);
            dynamic weatherInfo = !String.IsNullOrWhiteSpace(weatherJson) ? JsonConvert.DeserializeObject<dynamic>(weatherJson) : null;

            if (weatherInfo == null)
            {
                activity.Text = $"Could not get weather information for {city}";
                return activity;
            }

            // update header
            TextBlock dateHeader = (TextBlock)(((ColumnSet)card.Body[0]).Columns[0].Items[0]);
            dateHeader.Text = now.ToString("ddd MMM dd");
            
            // update updated time
            TextBlock updated = (TextBlock)card.Body[card.Body.Count - 1];
            updated.Text = $"Updated {now.ToShortTimeString()}";

            weatherInfo = weatherInfo.query.results.channel;
            dynamic wind = weatherInfo.wind;
            string cardinalDirection = Weather.DegreesToCardinal(double.Parse(wind.direction.ToString()));

            // update city
            TextBlock cityHeader = (TextBlock)(((ColumnSet)card.Body[0]).Columns[1].Items[0]);
            dynamic location = weatherInfo.location;
            cityHeader.Text = $"{location.city.ToString()}, {location.region.ToString()}";

            // update wind
            TextBlock windInfo = (TextBlock)card.Body[3];
            windInfo.Text = $"Winds {wind.speed} mph {cardinalDirection}";

            dynamic currentCondition = weatherInfo.item.condition;

            // update weather condition text and image
            TextBlock conditionSummary = (TextBlock)card.Body[2];
            conditionSummary.Text = currentCondition.text;

            Image currentWeatherImage = (Image)(((ColumnSet)card.Body[1]).Columns[0].Items[0]);
            currentWeatherImage.Url = GetWeatherImage(false, conditionSummary.Text);

            
            if (isForecast)
            {
                dynamic forecast = weatherInfo.item.forecast;
                dynamic currentDay = forecast[0];

                ((TextBlock)(((ColumnSet)card.Body[1]).Columns[1].Items[0])).Text = currentDay.high;
                ((TextBlock)(((ColumnSet)card.Body[1]).Columns[2].Items[0])).Text = currentDay.low;
            }
            else
            {
                ((TextBlock)(((ColumnSet)card.Body[1]).Columns[1].Items[0])).Text = currentCondition.temp;
                ((TextBlock)(((ColumnSet)card.Body[1]).Columns[2].Items[0])).Text = String.Empty;
            }

            activity.Attachments.Add(new Attachment(AdaptiveCard.ContentType, content: card));
            return activity;
        }
    }
}
