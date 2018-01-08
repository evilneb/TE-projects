using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.DataEntryModels
{
    public class DE_BeadStability
    {
        //constructor
        public DE_BeadStability()
        {
            Changed = false;
        }

        //Properties
        public bool Changed { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DatadsetId { get; set; }
        public string Strain { get; set; }
        public string FormTreatment { get; set; }
        public int Rep { get; set; }
        public string BeadAge { get; set; }

        //Properties that a technician can update
        public string Notes { get; set; }
        public List<string> DilutionResults { get; set; }
    }
}