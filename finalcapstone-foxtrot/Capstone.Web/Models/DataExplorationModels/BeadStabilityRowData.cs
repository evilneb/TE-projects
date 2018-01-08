using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.DataExplorationModels
{
    public class BeadStabilityRowData
    {
        public string Dataset_ID { get; set; }
        public string Strain { get; set; }
        public string FormulationTitration { get; set; }
        public string Rep { get; set; }
        public string BeadAge { get; set; }
        public string Notes { get; set; }
        public string Dil_1HR_Zero { get; set; }
        public string Dil_1HR_One { get; set; }
        public string Dil_1HR_Two { get; set; }
        public string Dil_1HR_Three { get; set; }
        public string Dil_1HR_Four { get; set; }
        public string Dil_1HR_Five { get; set; }
        public string Dil_1HR_Six { get; set; }
        public string Dil_1HR_Seven { get; set; }
        public string Dil_6HR_Zero { get; set; }
        public string Dil_6HR_One { get; set; }
        public string Dil_6HR_Two { get; set; }
        public string Dil_6HR_Three { get; set; }
        public string Dil_6HR_Four { get; set; }
        public string Dil_6HR_Five { get; set; }
        public string Dil_6HR_Six { get; set; }
        public string Dil_6HR_Seven { get; set; }
        public string Dil_6HR_Eight { get; set; }
        public string Dil_24HR_Zero { get; set; }
        public string Dil_24HR_One { get; set; }
        public string Dil_24HR_Two { get; set; }
        public string Dil_24HR_Three { get; set; }
        public string Dil_24HR_Four { get; set; }
        public string Dil_24HR_Five { get; set; }
        public string Dil_24HR_Six { get; set; }
        public string Dil_24HR_Seven { get; set; }
        public bool Outlier { get; set; }
    }
}
