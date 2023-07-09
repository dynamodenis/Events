using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace AptaEvents.Module.Helpers
{
    public class EventsApi
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static async Task<List<string>> GetEventsAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                logger.Info($"Getting events from : {url}");

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();

                    List<string> events = JsonConvert.DeserializeObject<List<string>>(results);
                    return events;
                }
                else
                {
                    logger.Error($"Error getting events list: {response.StatusCode}");
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }
        }
    }
}
