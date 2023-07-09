using AptaEvents.Module.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.Services
{
    public class Events
    {
        public async Task<List<string>> GetEvents()
        {
            List<string> events = await EventsApi.GetEventsAsync("url");
            //Call the events api

            return events;
        }
    }
}
