using ShoppingSite.Models;
using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = _dbContext.User.ToList();

            return View(users);
        }

        public ActionResult CreateUser()
        {
            var userVM = new UsersViewModel
            {
                Products = new Products(),
                User = new User()
            };

            return View("CreateUser", userVM);
        }

        [HttpPost]
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                var usersVM = new UsersViewModel
                {
                    User = user
                };

                return View("CreateUser", usersVM);
            }

            if (user.Id == 0)
            {
                _dbContext.User.Add(user);
            }

            else
            {
                var userInDb = _dbContext.User.Single(w => w.Id == user.Id);
                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.BirthDate = user.BirthDate;
                userInDb.Email = user.Email;
            }

            _dbContext.SaveChanges();


            return RedirectToAction("Index", "User");
        }
    }
}