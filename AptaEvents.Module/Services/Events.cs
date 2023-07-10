using AptaEvents.Module.BusinessObjects;
using AptaEvents.Module.Helpers;
using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptaEvents.Module.Services
{
    public class EventsServices
    {
        public static async Task<List<ApiEventList>> GetEvents(DateTime? eventDate, string eventName)
        {
            if (eventDate == null)
            {
                eventDate = DateTime.UtcNow.Date;
            }
            string url = "/api/Events/GetEventList?date=" + eventDate + "&eventName=" + eventName;
            
            List<ApiEventList> events = await EventsApi.GetEventsAsync(url);

            return events;
        }
    }
}
