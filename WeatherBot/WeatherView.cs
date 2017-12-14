using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Templates;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

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
                    { CURRENT, (context, city) => GetWeatherCard(context, city, false) },
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
            ""text"": ""60% chance of rain"",
            ""type"": ""TextBlock""
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
            ""text"": ""60% chance of rain"",
            ""type"": ""TextBlock""
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
        public WeatherView() : base(new DictionaryRenderer(Templates))
        {
        }

        public static IMessageActivity GetWeatherCard(BotContext context, string city, bool isForecast)
        {
            IMessageActivity activity = context.Request.CreateReply();
            AdaptiveCard card = JsonConvert.DeserializeObject<AdaptiveCard>(isForecast ? TemplateForecast : TemplateCurrent);
            activity.Attachments.Add(new Attachment(AdaptiveCard.ContentType, content: card));
            return activity;
        }
    }
}
