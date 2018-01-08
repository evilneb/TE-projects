using Capstone.Web.DataAccess;
using Capstone.Web.Models;
using Capstone.Web.Models.DataEntryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class DataEntryController : Controller
    {
        private readonly IDataEntrySqlDAL sqlDAL;

        public DataEntryController(IDataEntrySqlDAL sqlDAL)
        {
            this.sqlDAL = sqlDAL;
        }

        // GET: DataEntry
        public ActionResult Index()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View();
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult StorageDE()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_StorageStability> rows = sqlDAL.Storage_getAllRows();
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View(rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        [HttpPost]
        public ActionResult StorageDE(List<DE_StorageStability> rows)
        {
            foreach (DE_StorageStability thisRow in rows)
            {
                if (thisRow.Changed)
                {
                    sqlDAL.Update1Row(thisRow);
                }
            }
            return RedirectToAction("StorageDE");
        }


        public ActionResult BeadDE()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_BeadStability> rows = sqlDAL.Bead_getAllRows();
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View(rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        [HttpPost]
        public ActionResult BeadDE(List<DE_BeadStability> rows)
        {
            foreach (DE_BeadStability thisRow in rows)
            {
                if (thisRow.Changed)
                {
                    sqlDAL.Update1Row(thisRow);
                }
            }
            return RedirectToAction("BeadDE");
        }

        public ActionResult ChemDE()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_ChemCompatibility> rows = sqlDAL.Chem_getAllRows();
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View(rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        [HttpPost]
        public ActionResult ChemDE(List<DE_ChemCompatibility> rows)
        {
            foreach (DE_ChemCompatibility thisRow in rows)
            {
                if (thisRow.Changed)
                {
                    sqlDAL.Update1Row(thisRow);
                }
            }
            return RedirectToAction("ChemDE");
        }

        [HttpPost]
        public ActionResult DataEntryByDatasetId(string userDatasetId)
        {
            int dataId = 0;
            if (Int32.TryParse(userDatasetId, out dataId) && dataId > 0)
            {
                string exType = sqlDAL.GetTableByID(dataId);
                if (exType.Length > 0)
                {
                    if (exType.Equals("storage_stability"))
                    {
                        return RedirectToAction("StorDataId", new { id = dataId });
                    }
                    else if (exType.Equals("beads_stability"))
                    {
                        return RedirectToAction("BeadDataId", new { id = dataId });
                    }
                    else if (exType.Equals("chem_compatibility"))
                    {
                        return RedirectToAction("ChemDataId", new { id = dataId });
                    }

                }

            }

            return View("DatasetDNE");

        }


        public ActionResult StorDataId(int id)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_StorageStability> rows = sqlDAL.Storage_getDataset(id);
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View("StorageDE", rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult BeadDataId(int id)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_BeadStability> rows = sqlDAL.Bead_getDataset(id);
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View("BeadDE", rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult ChemDataId(int id)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Technician || (Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    List<DE_ChemCompatibility> rows = sqlDAL.Chem_getDataset(id);
                    if (rows.Count == 0)
                    {
                        return View("Error");
                    }
                    return View("ChemDE", rows);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }
    }
}