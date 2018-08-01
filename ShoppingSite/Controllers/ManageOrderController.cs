using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    [Authorize]
    public class ManageOrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ManageOrderController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: ManageOrder
        public ActionResult Index()
        {
            var orderHeaders = _dbContext.OrderHeaders.Where(w => w.Status != StaticDetails.StatusSent
            && w.Status != StaticDetails.StatusReady
            && w.Status != StaticDetails.StatusCancelled).ToList();

            var orderDetailsVM = new OrderDetailsViewModel()
            {
                OrderDetailsViewModels = new List<OrderDetailsViewModel>()
            };

            foreach (var order in orderHeaders)
            {
                var orderHeader = order;
                var orderDetailsList = _dbContext.OrderDetails.Where(w => w.OrderId == order.Id).ToList();

                var indevidual = new OrderDetailsViewModel()
                {
                    OrderHeader = orderHeader,
                    OrderDetails = orderDetailsList,
                };

                orderDetailsVM.OrderDetails = indevidual.OrderDetails;
                orderDetailsVM.OrderHeader = indevidual.OrderHeader;

                orderDetailsVM.OrderDetailsViewModels.Add(indevidual);
            }

            return View(orderDetailsVM);
        }

        [Authorize]
        public ActionResult OnPostOrderPrepare(int? Id)
        {
            var orderHeader = _dbContext.OrderHeaders.Find(Id);
            orderHeader.Status = StaticDetails.StatusPrepered;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult OnPostOrderCancel(int? Id)
        {
            var orderHeader = _dbContext.OrderHeaders.Find(Id);
            orderHeader.Status = StaticDetails.StatusCancelled;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult OnPostOrderReady(int? Id)
        {
            var orderHeader = _dbContext.OrderHeaders.Find(Id);
            orderHeader.Status = StaticDetails.StatusReady;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}