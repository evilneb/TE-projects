using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.DataExplorationModels
{
    public class ChemicalCompatibility
    {
        public string Dataset_ID { get; set; }
        public string Strain { get; set; }
        public string Chemical { get; set; }
        public string Rep { get; set; }
        public string Notes { get; set; }
        public string Rate_Dilution { get; set; }
        public string Dil_Zero { get; set; }
        public string Dil_One { get; set; }
        public string Dil_Two { get; set; }
        public string Dil_Three { get; set; }
        public string Dil_Four { get; set; }
        public string Dil_Five { get; set; }
        public string Dil_Six { get; set; }
        public string Dil_Seven { get; set; }
        public bool Outlier { get; set; }
    }
}