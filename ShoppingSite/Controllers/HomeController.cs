using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
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

            var shoppingCart = _dbContext.ShoppingCart.ToList();

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _dbContext.ShoppingCart.Where(w => w.ApplicationUserId == claim.Value);

            if (User.IsInRole(StaticRoles.IsAdmin) || User.Identity.IsAuthenticated) //TODO remove OR
            {
                Session["cart"] = shoppingCart;
                var cnt = Session["count"] = cart.Count();
                cnt = Convert.ToInt32(Session["count"]) + 1;

                return View(products);
            }

            return View("ReadOnlyIndex", products);
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

        [Authorize(Roles = StaticRoles.IsAdmin)]
        public ActionResult CreateProduct()
        {
            var productVM = new ProductsViewModel
            {
                CategoryTypes = _dbContext.CategoryTypes.ToList()
            };

            return View("CreateProduct", productVM);
        }

        [Authorize]
        public ActionResult AddProduct(int? id)
        {
            var products = _dbContext.Products.Include(w => w.CategoryType).SingleOrDefault(w => w.Id == id);

            var cartObj = new ShoppingCart
            {
                ProductsId = products.Id,
                Products = products,
                Count = +1
            };

            if (cartObj != null)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartObj.ApplicationUserId = claim.Value;

                ShoppingCart shoppingCartDb = _dbContext.ShoppingCart.
                    Where(w => w.ApplicationUserId == cartObj.ApplicationUserId && w.ProductsId == cartObj.ProductsId)
                    .FirstOrDefault();

                if (shoppingCartDb == null)
                {
                    _dbContext.ShoppingCart.Add(cartObj);
                }
                else
                {
                    shoppingCartDb.Count += cartObj.Count;
                }

                _dbContext.SaveChanges();
            }
            else
            {
                var productFromDb = _dbContext.Products.Include(w => w.CategoryType).FirstOrDefault();

                cartObj = new ShoppingCart
                {
                    ProductsId = products.Id,
                    Products = products
                };

                return View();
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        [Authorize(Roles = StaticRoles.IsAdmin)]
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
                productInDb.NumberAvailable = products.NumberAvailable;
                productInDb.StrapQuality = products.StrapQuality;
                productInDb.Price = products.Price;

                if (productInDb.NumberAvailable >= 1)
                {
                    productInDb.IsAvailableInStock = true;
                }
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = StaticRoles.IsAdmin)]
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

        [Authorize(Roles = StaticRoles.IsAdmin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var productInDb = _dbContext.Products.Find(id);

            if (productInDb == null)
            {
                return HttpNotFound();
            }

            _dbContext.Products.Remove(productInDb);

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}