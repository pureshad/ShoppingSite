using Microsoft.AspNet.SignalR;
using ShoppingSite.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingSite.SIgnalR.Hubs
{
    public class UserTrackHub : Hub
    {
        private static bool IsAdminActive { get; set; }

        public void Send(bool isOnline)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserTrackHub>();
            context.Clients.All.updateAdminOnlineStatus(isOnline);
        }

        public override Task OnConnected()
        {
            using (var db = new ApplicationDbContext())
            {
                var userLoged = Context.User.Identity.Name;
                if (!string.IsNullOrEmpty(userLoged))
                {
                    var claimsIdentity = (ClaimsIdentity)Context.User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                    var RoleAdmin = db.Roles.OrderBy(r => r.Name).FirstOrDefault(w => w.Name == StaticRoles.IsAdmin).Users.Where(w => w.RoleId
                             == w.RoleId).Select(w => w.UserId).FirstOrDefault();

                    if (RoleAdmin == claim)
                    {
                        IsAdminActive = true;
                        Send(IsAdminActive);
                    }
                    else
                    {
                        Send(IsAdminActive);
                    }
                }
                else
                {
                    Send(IsAdminActive = false);
                }
            }

            return base.OnConnected();
        }
    }
}