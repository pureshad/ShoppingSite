using Microsoft.AspNet.SignalR;
using ShoppingSite.Models;

namespace ShoppingSite.SIgnalR.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            using (var db = new ApplicationDbContext())
            {
                name = Context.User.Identity.Name;
            }
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}