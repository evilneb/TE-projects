using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DataAccess;
using Capstone.Web.Models.DataExplorationModels;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class DataExplorationController : Controller
    {
        private readonly IDataExplorationSqlDAL sqlDAL;

        public DataExplorationController(IDataExplorationSqlDAL sqlDAL)
        {
            this.sqlDAL = sqlDAL;
        }

        // GET: DataExploration
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

        public ActionResult StorageStability()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(sqlDAL.GetStorageStabilityData());
                }
                else if ((Session["currentUser"] as User).Partneruser)
                {

                    string partner = sqlDAL.GetPartner((Session["currentUser"] as User).Username);
                    return View(sqlDAL.GetSSforPartner(partner));

                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        //NOTE
        //Technically doesn't fully protect against a partner user getting summary data for datasets they are not associated with
        public ActionResult SummaryStorageStabilityView(string strain, string dpa, string dataset_id)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator || (Session["currentUser"] as User).Partneruser)
                {
                    List<StorageStabilityExperimentData> summaryStorageStabilityData = sqlDAL.GetSummaryStorageStabilityData(strain, dpa, dataset_id);
                    List<List<StorageStabilityExperimentData>> chunkList = CFUSummaryDataCalculationForStorageStability.ChunkListGeneration(summaryStorageStabilityData);
                    List<List<StorageStabilityExperimentData>> chunkListWithCFUAverages = CFUSummaryDataCalculationForStorageStability.CalcualteAndAddAverageCFUValueToEachSubListAndReturnThatChunkList(chunkList);

                    return View(chunkListWithCFUAverages);
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }

        public ActionResult BeadStability()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(sqlDAL.GetBeadStabilityData());
                }
                else if ((Session["currentUser"] as User).Partneruser)
                {

                    string partner = sqlDAL.GetPartner((Session["currentUser"] as User).Username);
                    return View(sqlDAL.GetBSforPartner(partner));
                }
            }
            return RedirectToAction("AccessDenied", "User");

        }

        //NOTE
        //Technically doesn't fully protect against a partner user getting summary data for datasets they are not associated with
        public ActionResult SummaryBeadStabilityView(string strain, string dataset_id_1, string dataset_id_2, string hours, string formTreatment1, string formTreatment2)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator || (Session["currentUser"] as User).Partneruser)
                {
                    ViewData["hour"] = hours;
                    List<BeadStabilityRowData> bigListOfSummaryBeadStabilityData = sqlDAL.GetSummaryBeadStabilityData(strain, dataset_id_1, dataset_id_2, hours, formTreatment1, formTreatment2);
                    List<List<BeadStabilityRowData>> chunkListOfBeadStabilitySummaryData = new List<List<BeadStabilityRowData>>();
                    chunkListOfBeadStabilitySummaryData = CFUSummaryDataCalculationForBeadStability.GenerateChunkListOfSummaryBeadStabilityData(bigListOfSummaryBeadStabilityData);
                    List<List<BeadStabilityRowData>> updatedChunkListBeadStability = CFUSummaryDataCalculationForBeadStability.CalculateAverageCFUValueToEachSublist(chunkListOfBeadStabilitySummaryData);
                    return View(updatedChunkListBeadStability);
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }

        public ActionResult ChemicalCompatibility()
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator)
                {
                    return View(sqlDAL.GetChemicalCompatibilityData());
                }
                else if ((Session["currentUser"] as User).Partneruser)
                {

                    string partner = sqlDAL.GetPartner((Session["currentUser"] as User).Username);
                    return View(sqlDAL.GetCCforPartner(partner));
                }
            }
            return RedirectToAction("AccessDenied", "User");
        }


        //NOTE
        //Technically doesn't fully protect against a partner user getting summary data for datasets they are not associated with
        public ActionResult SummaryChemicalCompatibility(string strain, string dataset_id, string chemical1, string chemical2, string rate_dil)
        {
            if (Session["currentUser"] != null)
            {
                if ((Session["currentUser"] as User).Researcher || (Session["currentUser"] as User).Administrator || (Session["currentUser"] as User).Partneruser)
                {
                    List<ChemicalCompatibility> summaryChemicalCompatibiltyData = sqlDAL.GetSummaryChemicalCompatibilityData(strain, dataset_id, chemical1, chemical2, rate_dil);
                    List<List<ChemicalCompatibility>> chunkListOfChemicalCompatibility = new List<List<ChemicalCompatibility>>();
                    chunkListOfChemicalCompatibility = CFUSummaryDataCalculationForChemicalCompatibility.ChunkListGeneration(summaryChemicalCompatibiltyData);
                    chunkListOfChemicalCompatibility = CFUSummaryDataCalculationForChemicalCompatibility.CalcualteAndAddAverageCFUValueToEachSubListAndReturnThatChunkList(chunkListOfChemicalCompatibility);
                    return View(chunkListOfChemicalCompatibility);
                }
            }
            return RedirectToAction("AccessDenied", "User");


        }



    }
}