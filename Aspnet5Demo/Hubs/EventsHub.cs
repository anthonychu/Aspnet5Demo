using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Aspnet5Demo.Models;

namespace Aspnet5Demo.Hubs
{
    public class EventsHub : Hub
    {
        public static void SendEventUpdate(int eventId, int newCount)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<EventsHub>();
            hub.Clients.All.updateEvent(eventId, newCount);
        }
    }
}