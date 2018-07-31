using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var CartDetailsVM = new OrderDetailsCartViewModel
            {
                OrderHeader = new OrderHeader()
            };

            var shoppingCart = _dbContext.ShoppingCart.ToList();

            GetShoppingCartQuantity(shoppingCart);

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

        private void GetShoppingCartQuantity(List<ShoppingCart> shoppingCart)
        {
            Session["cart"] = shoppingCart;
            var cnt = Session["count"] = shoppingCart.Count;
            cnt = Convert.ToInt32(Session["count"]) + 1;
        }

        public ActionResult PlusProduct(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var shoppingCartItem = _dbContext.ShoppingCart.Where(w => w.Id == id).FirstOrDefault();

            shoppingCartItem.Count++;

            _dbContext.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult MinusProduct(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var shoppingCartItem = _dbContext.ShoppingCart.Where(w => w.Id == id).FirstOrDefault();

            if (shoppingCartItem.Count == 1)
            {
                _dbContext.ShoppingCart.Remove(shoppingCartItem);
                _dbContext.SaveChanges();
            }
            else
            {
                shoppingCartItem.Count--;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PlaceOrder()
        {
            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var shoppingCart = _dbContext.ShoppingCart.Where(w => w.ApplicationUserId == claim.Value).ToList();


            var CartDetailsVM = new OrderDetailsCartViewModel
            {
                ShoppingCarts = shoppingCart,
                OrderHeader = new OrderHeader()
            };

            OrderHeader orderHeader = CartDetailsVM.OrderHeader;
            CartDetailsVM.OrderHeader.OrderDate = DateTime.Now;
            CartDetailsVM.OrderHeader.UserId = claim.Value;
            CartDetailsVM.OrderHeader.Status = StaticDetails.StatusSubmited;

            _dbContext.OrderHeaders.Add(orderHeader);
            _dbContext.SaveChanges();

            foreach (var item in CartDetailsVM.ShoppingCarts)
            {
                item.Products = _dbContext.Products.FirstOrDefault(w => w.Id == item.ProductsId);
                CartDetailsVM.OrderHeader.OrderTotal += (item.Products.Price * item.Count);


                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductsId = item.ProductsId,
                    OrderId = orderHeader.Id,
                    Name = item.Products.Name,
                    Price = item.Products.Price,
                    Count = item.Count,
                    Description = item.Products.Description,
                    OrderHeader = orderHeader.Comments
                };

                item.Products.NumberAvailable--;

                if (item.Products.NumberAvailable == 0)
                {
                    item.Products.IsAvailableInStock = false;
                }

                _dbContext.OrderDetails.Add(orderDetail);
            }
            _dbContext.ShoppingCart.RemoveRange(CartDetailsVM.ShoppingCarts);

            Session["cart"] = CartDetailsVM.ShoppingCarts;
            var cnt = Session["count"] = CartDetailsVM.ShoppingCarts.Count;
            cnt = Convert.ToInt32(Session["count"]);
            cnt = 0;

            _dbContext.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = orderHeader.Id });
        }

        public ActionResult OrderConfirmation(int id)
        {
            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var shoppingCart = _dbContext.ShoppingCart.Where(w => w.ApplicationUserId == claim.Value).ToList();

            var orderDetailsVM = new OrderDetailsViewModel()
            {
                OrderHeader = _dbContext.OrderHeaders.Where(w => w.Id == id).FirstOrDefault(),
                OrderDetails = _dbContext.OrderDetails.Where(w => w.OrderId == id).ToList()
            };

            Session["cart"] = shoppingCart;
            var cnt = Session["count"] = shoppingCart.Count;
            cnt = Convert.ToInt32(Session["count"]);

            return View(orderDetailsVM);
        }
    }
}