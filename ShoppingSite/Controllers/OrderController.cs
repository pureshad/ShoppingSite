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
        [Authorize]
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

        [Authorize]
        public ActionResult OrderPickupDetails(int? id)
        {
            var orderDetailsVM = new OrderDetailsViewModel()
            {
                OrderDetailsViewModels = new List<OrderDetailsViewModel>()
            };

            orderDetailsVM.OrderHeader = _dbContext.OrderHeaders.
                Where(w => w.Id == id).FirstOrDefault();

            orderDetailsVM.OrderHeader.ApplicationUser = _dbContext.Users.
                Where(w => w.Id == orderDetailsVM.OrderHeader.UserId).FirstOrDefault();

            orderDetailsVM.OrderDetails = _dbContext.OrderDetails.
                Where(w => w.OrderId == orderDetailsVM.OrderHeader.Id).ToList();

            return View(orderDetailsVM);
        }

        [Authorize]
        public ActionResult OrderPickup(string option = null, string search = null)
        {
            var orderDetailsVM = new OrderDetailsViewModel()
            {
                OrderDetailsViewModels = new List<OrderDetailsViewModel>()
            };

            if (search != null)
            {
                var user = new ApplicationUser();

                List<OrderHeader> orderHeadersList = _dbContext.OrderHeaders.Where(w => w.UserId == user.Id).ToList();

                if (option == "order")
                {
                    orderHeadersList = _dbContext.OrderHeaders.Where(o => o.Id == Convert.ToInt32(search)).ToList();
                }
                else
                {
                    if (option == "email")
                    {
                        user = _dbContext.Users.Where(u => u.Email.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                    }
                    else if (option == "phonenumber")
                    {
                        user = _dbContext.Users.Where(u => u.PhoneNumber.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                    }
                    else if (option == "name")
                    {
                        user = _dbContext.Users.Where(u => u.FirstName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                        || u.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).FirstOrDefault();
                    }
                }
                if (user != null || orderHeadersList.Count > 0)
                {
                    if (orderHeadersList.Count == 0)
                    {
                        orderHeadersList = _dbContext.OrderHeaders.Where(w => w.UserId == user.Id).ToList();
                    }

                    foreach (OrderHeader item in _dbContext.OrderHeaders.Where(w => w.Status != StaticDetails.StatusReady).ToList())
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

                }
            }
            else
            {
                var orderHeadersList = _dbContext.OrderHeaders.Where(w => w.Status == StaticDetails.StatusReady).ToList();

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
            }

            return View(orderDetailsVM);
        }

        [Authorize]
        public ActionResult ConfirmOrder(int? id)
        {
            var orderHeader = _dbContext.OrderHeaders.Find(id);
            orderHeader.Status = StaticDetails.StatusSent;
            _dbContext.SaveChanges();

            return RedirectToAction("/Index");
        }
    }
}