using Capstone.Web.DataAccess;
using Capstone.Web.Models.ExperimentForms;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ExperimentFormController : Controller
    {
        private readonly IExperimentSqlDAL sqlDAL;

        public ExperimentFormController(IExperimentSqlDAL sqlDAL)
        {
            this.sqlDAL = sqlDAL;
        }

        // GET: ExperimentForm
        public ActionResult Index()
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


        //STORAGE STABILITY 
        public ActionResult CreateStorageStabilityForm()
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

        [HttpPost]
        public ActionResult CreateStorageStabilityForm(StorageStabilityForm storageStForm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateStorageStabilityForm", storageStForm);
            }

            int[] results = sqlDAL.CreateStorageStabilityForm(DateTime.Now, storageStForm);
            //error view????
            Session["lastSavedFormID"] = results[0];
            return RedirectToAction("StorageStabilityConfirm", "ExperimentForm", storageStForm);
        }

        public ActionResult StorageStabilityConfirm(StorageStabilityForm newForm)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(newForm);
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }


        public ActionResult CreateBeadStabilityForm()
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

        [HttpPost]
        public ActionResult CreateBeadStabilityForm(BeadStabilityForm beadStForm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateBeadStabilityForm", beadStForm);
            }

            int[] results = sqlDAL.CreateBeadStabilityForm(DateTime.Now, beadStForm);
            //error view????
            Session["lastSavedFormID"] = results[0];
            return RedirectToAction("BeadStabilityConfirm", "ExperimentForm", beadStForm);
        }

        //CHEM COMPAT
        public ActionResult CreateChemCompatForm()
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

        [HttpPost]
        public ActionResult CreateChemCompatForm(ChemCompatForm chemForm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateChemCompatForm", chemForm);
            }

            int[] results = sqlDAL.CreateChemicalCompatibilityForm(DateTime.Now, chemForm);
            //error message???
            Session["lastSavedFormID"] = results[0];
            return RedirectToAction("ChemCompatConfirm", "ExperimentForm", chemForm);
        }

        public ActionResult ChemCompatConfirm(ChemCompatForm newForm)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(newForm);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult BeadStabilityConfirm(BeadStabilityForm newForm)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(newForm);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult DatasetInfo()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DatasetInfo> data = sqlDAL.GetDatasetInfo();
                    return View(data);
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }

        [HttpPost]
        public ActionResult DatasetInfo(List<DatasetInfo> data)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DatasetInfo> updated = (from i in data
                                                 where i.Changed == true
                                                 select i).ToList();
                    if (updated != null)
                    {
                        bool success = sqlDAL.UpdateDataSetInfo(updated);
                    }

                    return RedirectToAction("DatasetInfoSuccess", "ExperimentForm");
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }

        public ActionResult DatasetInfoSuccess()
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
    }
}