using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.Models.DataExplorationModels;
using System.Data.SqlClient;

namespace Capstone.Web.DataAccess
{
    public class Helpers
    {
        public DatasetInfo MakeDataset(SqlDataReader results)
        {
            DatasetInfo data = new DatasetInfo()
            {
                ExperimentID = results["id"].ToString(),
                ExperimentType = results["experiment_type"].ToString(),
                CreatedDate = (DateTime)results["created_date"],
                PartnerName = results["partner"].ToString(),
                FormulationDescription = results["formulation_description"].ToString(),
                Notes = results["notes"].ToString()
            };

            return data;
        }

        public StorageStabilityExperimentData MakeSSExploreModel(SqlDataReader results)
        {
            StorageStabilityExperimentData data = new StorageStabilityExperimentData()
            {
                Dataset_ID = results["dataset_id"].ToString(),
                Strain = results["strain"].ToString(),
                FormTreatments = results["form_trt"].ToString(),
                Rep = results["rep"].ToString(),
                PreActivationAges = results["preactivation_age"].ToString(),
                Temp = results["temp"].ToString(),
                DPA = results["dpa"].ToString(),
                Notes = results["notes"].ToString(),
                Dil_Zero = results["dil_zero"].ToString(),
                Dil_One = results["dil_neg1"].ToString(),
                Dil_Two = results["dil_neg2"].ToString(),
                Dil_Three = results["dil_neg3"].ToString(),
                Dil_Four = results["dil_neg4"].ToString(),
                Dil_Five = results["dil_neg5"].ToString(),
                Dil_Six = results["dil_neg6"].ToString(),
                Dil_Seven = results["dil_neg7"].ToString(),
                Outlier = (bool)results["outlier"]
            };

            return data;
        }
    }
}