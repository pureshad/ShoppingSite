using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: Order
        public ActionResult Index(int? id)
        {
            var claimIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orderDetails = _dbContext.OrderDetails.FirstOrDefault(w => w.OrderId == id);

            var orderDetailsVM = new OrderDetailsViewModel()
            {
                OrderDetailsViewModels = new List<OrderDetailsViewModel>()
            };

            var orderHeadersList = _dbContext.OrderHeaders.Where(w => w.UserId == claim.Value).ToList();

            if (id == 0 && orderHeadersList.Count > 4)
            {
                orderHeadersList = orderHeadersList.Take(5).ToList();
            }

            foreach (OrderHeader item in orderHeadersList)
            {
                var orderHeader = item;
                var orderDetailsList = _dbContext.OrderDetails.Where(w => w.OrderId == item.Id).ToList();

                var indevidual = new OrderDetailsViewModel()
                {
                    OrderHeader = orderHeader,
                    OrderDetails = orderDetailsList,
                };

                orderDetailsVM.OrderDetails = indevidual.OrderDetails;
                orderDetailsVM.OrderHeader = indevidual.OrderHeader;

                orderDetailsVM.OrderDetailsViewModels.Add(indevidual);

            }
            return View("Index", orderDetailsVM);
        }
    }
}