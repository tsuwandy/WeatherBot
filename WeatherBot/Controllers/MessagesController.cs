using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Storage;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;

namespace WeatherBot.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        public static BotFrameworkAdapter activityAdapter = null;
        public static Bot bot = null;

        public MessagesController(IConfiguration configuration)
        {
            if (activityAdapter == null)
            {
                string appId = configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value;
                string appPassword = configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppPasswordKey)?.Value;

                // create the activity adapter to send/receive Activity objects 
                activityAdapter = new BotFrameworkAdapter(appId, appPassword);
                bot = new Bot(activityAdapter)
                    .Use(new MemoryStorage())
                    .Use(new BotStateManager()) // --- add Bot State Manager to automatically persist and load the context.State.Conversation and context.State.User objects
                    .Use(new WeatherView())
                    .OnReceive(async (context) =>
                    {
                        // THIS IS YOUR BOT LOGIC
                        if (context.Request.Type == ActivityTypes.Message)
                        {
                            string text = context.Request.Text.ToLower();
                            string city = Weather.GetCity(text);
                            if (!string.IsNullOrWhiteSpace(city))
                            {
                                if (text.Contains("forecast"))
                                {
                                    //context.Reply(await Weather.GetWeatherForecastByCityName(city));
                                    context.ReplyWith(WeatherView.FORECAST, city);
                                }
                                else
                                {
                                    context.ReplyWith(WeatherView.CURRENT, city);
                                }
                            }
                            else
                            {
                                context.Reply("Please specify city");
                            }
                        }
                        else
                        {
                            context.Reply("Type city name to get weather");
                        }
                    });            
            }
        }

        [Authorize(Roles = "Bot")]
        [HttpPost]
        public async void Post([FromBody]Activity activity)
        {
            await activityAdapter.Receive(HttpContext.Request.Headers, activity);
        }
    }
}