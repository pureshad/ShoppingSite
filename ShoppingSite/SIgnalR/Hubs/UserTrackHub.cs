using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ShoppingSite.SIgnalR.Hubs
{
    public class UserTrackHub : Hub
    {
        private static int _userCount = 0;

        public override Task OnConnected()
        {
            _userCount++;
            var context = GlobalHost.ConnectionManager.GetHubContext<UserTrackHub>();
            return context.Clients.All.online(_userCount);
        }

        public override Task OnReconnected()
        {
            _userCount++;
            var context = GlobalHost.ConnectionManager.GetHubContext<UserTrackHub>();
            return context.Clients.All.online(_userCount);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _userCount--;
            var context = GlobalHost.ConnectionManager.GetHubContext<UserTrackHub>();
            return context.Clients.All.online(_userCount);
        }
    }
}