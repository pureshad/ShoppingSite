using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        public ActionResult Index()
        {
            var CartDetailsVM = new OrderDetailsViewModel
            {
                OrderHeader = new OrderHeader()
            };

            CartDetailsVM.OrderHeader.OrderTotal = 0;
            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _dbContext.ShoppingCart.Where(w => w.ApplicationUserId == claim.Value);

            if (cart != null)
            {
                CartDetailsVM.ShoppingCarts = cart.ToList();
            }

            foreach (var item in CartDetailsVM.ShoppingCarts)
            {
                item.Products = _dbContext.Products.FirstOrDefault(w => w.Id == item.ProductsId);
                CartDetailsVM.OrderHeader.OrderTotal += (item.Products.Price * item.Count);

                if (item.Products.Description.Length > 100)
                {
                    item.Products.Description = item.Products.Description.Substring(0, 99) + "...";
                }
            }

            return View(CartDetailsVM);
        }
    }
}