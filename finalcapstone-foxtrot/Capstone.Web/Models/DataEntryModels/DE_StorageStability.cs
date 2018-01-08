using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.DataEntryModels
{
    //This model represents one row of data from te storage_stabilty table in the 3bar_foxtrot database
    public class DE_StorageStability
    {
        //constructor
        public DE_StorageStability()
        {
            Changed = false;
            //SearchId = 0;
        }

        //Properties
        //public int SearchId { get; set; }
        public bool Changed { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DatadsetId { get; set; }
        public string Strain { get; set; }
        public string FormTreatment { get; set; }
        public int Rep { get; set; }
        public string PreactivationAge { get; set; }
        public string Temp { get; set; }
        public string DPA { get; set; }

        //Properties that a technician can update
        public string Notes { get; set; }
        public List<string> DilutionResults { get; set; }


    }
}