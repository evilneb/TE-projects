using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DataAccess;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserSqlDAL userSQLDAL;
        public HomeController(IUserSqlDAL userSQLDAL)
        {
            this.userSQLDAL = userSQLDAL;
        }
       // GET: Home
        public ActionResult Index()
        {
            Session.Abandon();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (userSQLDAL.UsernameExists(user.Username) && userSQLDAL.PasswordIsCorrect(user.Username, user.Password))
            {
                User userWithRoles = new User();

                userWithRoles.Username = user.Username;
                if (userSQLDAL.IsAdmin(user.Username))
                {
                    userWithRoles.Administrator = true;
                }
                if (userSQLDAL.IsTechnician(user.Username))
                {
                    userWithRoles.Technician = true;
                }
                if (userSQLDAL.IsResearcher(user.Username))
                {
                    userWithRoles.Researcher = true;
                }
                if (userSQLDAL.IsPartner(user.Username))
                {
                    userWithRoles.Partneruser = true;
                }
                Session["currentUser"] = userWithRoles;

                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("UserName", "Username or password incorrect or doesn't exist");

            return View("Index", user);
        }
    }
}