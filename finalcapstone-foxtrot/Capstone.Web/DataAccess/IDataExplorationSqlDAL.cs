using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Capstone.Web.Models.DataExplorationModels;

namespace Capstone.Web.DataAccess
{
    public interface IDataExplorationSqlDAL
    {
        List<StorageStabilityExperimentData> GetStorageStabilityData();
        List<BeadStabilityRowData> GetBeadStabilityData();
        List<ChemicalCompatibility> GetChemicalCompatibilityData();
        List<StorageStabilityExperimentData> GetSSforPartner(string partnerName);
        List<BeadStabilityRowData> GetBSforPartner(string partnerName);
        List<ChemicalCompatibility> GetCCforPartner(string partnerName);
        List<StorageStabilityExperimentData> GetSummaryStorageStabilityData(string strain, string dpa, string dataset_id);
        List<BeadStabilityRowData> GetSummaryBeadStabilityData(string strain, string dataset_id_1, string dataset_id_2, string hours, string formTreatment1, string formTreatment2);

        List<ChemicalCompatibility> GetSummaryChemicalCompatibilityData(string strain, string dataset_id, string chemical1, string chemical2, string rate_dil);
        string GetPartner(string username);
    }
}
