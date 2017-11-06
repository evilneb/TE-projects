using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Park
    {
        //Properties
        public string ParkCode { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Acreage { get; set; }
        public string Elevation { get; set; }
        public string MilesOfTrail { get; set; }
        public string NumCampsites { get; set; }
        public string Climate { get; set; }
        public string YearFounded { get; set; }
        public string AnnualVisitorCount { get; set; }
        public string Quote { get; set; }
        public string QuoteSource { get; set; }
        public string Description { get; set; }
        public string EntryFee { get; set; }
        public string NumAnimalSpecies { get; set; }

        
    }
}