using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DataAccess;
using System.Configuration;
using Capstone.Web.Models.ExperimentForms;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserSqlDAL sqlDAL;

        public UserController(IUserSqlDAL sqlDAL)
        {
            this.sqlDAL = sqlDAL;
        }

        // GET: User

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminDetail()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult ResearcherDetail()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult CreateUser()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUser", newUser);
            }

            if (sqlDAL.UsernameExists(newUser.Username))
            {
                return View("NewUserError");
            }

            sqlDAL.SaveUser(newUser);


            return View("SuccessUserAdded", newUser);
        }

        public ActionResult AdminChangeUserPassword()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        [HttpPost]
        public ActionResult AdminChangeUserPassword(User passwordChangeUser)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminChangeUserPassword", passwordChangeUser);
            }

            if (sqlDAL.UsernameExists(passwordChangeUser.Username) && sqlDAL.ChangeUsersPassword(passwordChangeUser.Username, passwordChangeUser.Password))
            {
                return RedirectToAction("PasswordChangedConfirm", "User", passwordChangeUser);
            }
            else
            {
                return View("AdminPasswordChangeError");
            }
        }

        public ActionResult PasswordChangedConfirm(User passwordChangeUser)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    return View(passwordChangeUser);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult RemoveUser()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }


        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
