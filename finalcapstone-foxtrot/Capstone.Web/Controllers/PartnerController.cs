using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DataAccess;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class PartnerController : Controller
    {
        private readonly IPartnerSqlDAL sqlDAL;

        public PartnerController(IPartnerSqlDAL partnerSqlDAL)
        {
            this.sqlDAL = partnerSqlDAL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePartner()
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
        public ActionResult CreatePartner(Partner newPartner)
        {
            string partnerToCheck = Convert.ToString(newPartner.PartnerName);
            bool preexistingPartner = sqlDAL.PartnerExists(partnerToCheck);

            if (preexistingPartner)
            {
                ViewBag.ErrorMessage = "*This partner has already been added.";

                return View("CreatePartner", newPartner);
            }

            if (!ModelState.IsValid)
            {
                return View("CreatePartner", newPartner);
            }

            sqlDAL.AddPartner(newPartner);

            return View("SuccessPartnerAdded", newPartner);

        }

        public ActionResult AssignPartnerUser()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Administrator)
                {
                    AssignPartnerUser dropDowns = new AssignPartnerUser();
                    dropDowns.UserDropDownList = sqlDAL.GetUserList();
                    dropDowns.PartnerDropDownList = sqlDAL.GetPartnerList();
                    return View(dropDowns);
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }

        [HttpPost]
        public ActionResult AssignPartnerUser(AssignPartnerUser partnerUser)
        {
            if (!ModelState.IsValid)
            {
                return View("CreatePartner", partnerUser);
            }

            sqlDAL.AssignPartnerUser(partnerUser);

            return View("SuccessPartnerUserAssigned", partnerUser);
        }


        // GET: Partner/RemovePartner/id
        public ActionResult RemovePartner(int id)
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

        // POST: Partner/RemovePartner/id
        [HttpPost]
        public ActionResult RemovePartner(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
