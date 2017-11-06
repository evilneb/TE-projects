using Capstone.Web.Dal;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IParkWeatherDal applicationDal;

        public HomeController(IParkWeatherDal applicationDal)
        {
            this.applicationDal = applicationDal;
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Park> parks = applicationDal.GetParks();
            return View(parks);
        }

        public ActionResult Temp()
        {
            string unit = Request["Temp"];
            Session["convertToCelsius"] = unit;
            Park park = Session["currentPark"] as Park;
            return RedirectToAction("Detail", park);
        }

        public ActionResult Detail(Park park)
        {
            Session["currentPark"] = park;
            Session["forecast"] = applicationDal.GetForecasts(park.ParkCode);
            return View(park);
        }


        public ActionResult Survey()
        {
            Session["parks"] = applicationDal.GetParks();
            return View();
        }

        [HttpPost]
        public ActionResult Survey(Survey model)
        {
            if (!ModelState.IsValid)
            {
                return View("Survey", model);
            }
            else
            {
                
                bool success = applicationDal.SaveSurvey(model);
                return RedirectToAction("MostPopPark");
            }
        }

        public ActionResult MostPopPark()
        {
            Park mostPop = applicationDal.MostPopularBySurvey();
            return View(mostPop);
        }


    }
}