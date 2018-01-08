using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.DataExplorationModels
{
    public class TempSSdata
    {
        public string ID { get; set; }
        public string Strain { get; set; }
        public string FormTreatment { get; set; }
        public string Rep { get; set; }
        public string PreactivationAge { get; set; }
        public string Temp { get; set; }
        public string DPA { get; set; }
        public string Notes { get; set; }
        public string Dil_0 { get; set; }
        public string Dil_1 { get; set; }
        public string Dil_2 { get; set; }
        public string Dil_3 { get; set; }
        public string Dil_4 { get; set; }
        public string Dil_5 { get; set; }
        public string Dil_6 { get; set; }
        public string Dil_7 { get; set; }
        public bool Outlier { get; set; }

    }
}