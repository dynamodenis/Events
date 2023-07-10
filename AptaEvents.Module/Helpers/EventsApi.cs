using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System.Diagnostics;
using AptaEvents.Module.BusinessObjects;

namespace AptaEvents.Module.Helpers
{
    public class EventsApi
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static async Task<List<ApiEventList>> GetEventsAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                logger.Info($"Getting events from : {url}");

                string host = "https://localhost:44337";

                HttpResponseMessage response = await client.GetAsync(host+url);

                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    List<ApiEventList> events = JsonConvert.DeserializeObject<List<ApiEventList>>(results);
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
