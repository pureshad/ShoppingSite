using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        public ActionResult Index()
        {
            var products = _dbContext.Products.Include(w => w.CategoryType).ToList();

            return View(products);
        }

        public ActionResult Details(int? id)
        {
            var product = _dbContext.Products.Include(w => w.CategoryType).SingleOrDefault(w => w.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult CreateProduct()
        {
            var productVM = new ProductsViewModel
            {
                CategoryTypes = _dbContext.CategoryTypes.ToList()
            };

            return View("CreateProduct", productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Products products)
        {
            products.CategoryType = _dbContext.CategoryTypes.SingleOrDefault(w => w.Id == products.CategoryTypeId);

            if (!ModelState.IsValid)
            {
                var productVM = new ProductsViewModel(products)
                {
                    CategoryTypes = _dbContext.CategoryTypes.ToList()
                };

                return View("CreateProduct", productVM);
            }

            //var product = _dbContext.Products.SingleOrDefault(w => w.Id == products.Id);

            if (products.Id == 0)
            {
                _dbContext.Products.Add(products);
            }

            else
            {
                var productInDb = _dbContext.Products.Single(w => w.Id == products.Id);
                productInDb.Name = products.Name;
                productInDb.Description = products.Description;
                productInDb.CreatedTime = products.CreatedTime;
                productInDb.Image = products.Image;
                productInDb.CategoryTypeId = products.CategoryTypeId;
                productInDb.Brand = products.Brand;
                productInDb.IsAvailableInStock = products.IsAvailableInStock;
                productInDb.NumberAvailable = products.NumberAvailable;
                productInDb.StrapQuality = products.StrapQuality;
                productInDb.Price = products.Price;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            var product = _dbContext.Products.Include(w => w.CategoryType).SingleOrDefault(w => w.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var productVM = new ProductsViewModel(product)
            {
                CategoryTypes = _dbContext.CategoryTypes.ToList(),
                Price = product.Price,
                StrapQuality = product.StrapQuality,
                DisplayType = product.DisplayType,
                Image = product.Image
            };

            return View("Edit", productVM);
        }
    }
}