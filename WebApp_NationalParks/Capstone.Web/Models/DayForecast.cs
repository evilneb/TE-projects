using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class DayForecast
    {
        public string ParkCode { get; set; }
        public string DaysOut { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        public List<string> Recommendation
        {
            get
            {
                List<string> result = new List<string>();
                if (Forecast.Contains("snow"))
                {
                    result.Add("Bring your snow shoes!");
                }
                if (Forecast.Contains("sunny"))
                {
                    result.Add("Bring sunscreen!");
                }
                if (Forecast.Contains("rain"))
                {
                    result.Add("Pack your rain gear and wear waterproof shoes!");
                }
                if (Forecast.Contains("thunderstorms"))
                {
                    result.Add("Seek shelter and avoid hiking on exposed ridges!");
                }
                if (High > 75)
                {
                    result.Add("Bring an extra gallon of water!");
                }
                if (High - Low > 20)
                {
                    result.Add("Wear breathable layers!");
                }
                if (Low < 20)
                {
                    result.Add("Frostbite warning! Dress appropriately.");
                }
                return result;
            }
        }
    }
}