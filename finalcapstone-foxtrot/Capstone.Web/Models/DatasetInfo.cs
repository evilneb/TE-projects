using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class DatasetInfo
    {
        public DatasetInfo()
        {
            Changed = false;
        }

        public bool Changed { get; set; }
        public string ExperimentID { get; set; }
        public string ExperimentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PartnerName { get; set; }
        public string FormulationDescription { get; set; }
        public string Notes { get; set; }
    }
}