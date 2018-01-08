using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models.ExperimentForms;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DataAccess
{
    public interface IExperimentSqlDAL
    {
        int[] CreateStorageStabilityForm(DateTime now, StorageStabilityForm newForm);

        int[] CreateBeadStabilityForm(DateTime now, BeadStabilityForm bSForm);

        int[] CreateChemicalCompatibilityForm(DateTime now, ChemCompatForm newForm);

        List<DatasetInfo> GetDatasetInfo();

        bool UpdateDataSetInfo(List<DatasetInfo> data);

        
        

    }
}