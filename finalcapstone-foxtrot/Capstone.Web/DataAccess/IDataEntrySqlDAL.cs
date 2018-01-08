using Capstone.Web.Models.DataEntryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DataAccess
{
    public interface IDataEntrySqlDAL
    {
        //Storage Stability Methods
        List<DE_StorageStability> Storage_getAllRows();

        bool Update1Row(DE_StorageStability row);

        List<DE_StorageStability> Storage_getDataset(int id);

        //Bead Stability Methods
        List<DE_BeadStability> Bead_getAllRows();

        bool Update1Row(DE_BeadStability row);


        //Chemical Compatibility Methods
        List<DE_ChemCompatibility> Chem_getAllRows();

        bool Update1Row(DE_ChemCompatibility row);

        //new stuff to do data entry by dataset ID
        string GetTableByID(int datasetId);


        List<DE_BeadStability> Bead_getDataset(int id);

        List<DE_ChemCompatibility> Chem_getDataset(int id);
    }
}
