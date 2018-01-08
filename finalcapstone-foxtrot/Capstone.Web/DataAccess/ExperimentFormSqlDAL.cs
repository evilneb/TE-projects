using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models.ExperimentForms;
using Capstone.Web.Models;
using Capstone.Web.DataAccess;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Capstone.Web.DataAccess
{
    public class ExperimentFormSqlDAL : IExperimentSqlDAL
    {
        private readonly string connectionString;
        private const string SQL_AddDataset = "insert into datasets (experiment_type, created_date) values(@expType, @now); select CAST(SCOPE_IDENTITY() as int);";

        private const string SQL_GetDatasetInfo = "SELECT * FROM datasets";
        private const string SQL_UpdateDatasetInfo = "UPDATE datasets SET partner = @partnerName, formulation_description = @formulationDescription, notes = @notes WHERE id = @experimentID";

        public ExperimentFormSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        //Create Storage Stability Form
        public int[] CreateStorageStabilityForm(DateTime now, StorageStabilityForm newForm)
        {
            int datasetId = 0;
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_AddDataset, conn);
                    cmd.Parameters.AddWithValue("@now", now);
                    cmd.Parameters.AddWithValue("@expType", "storage_stability");
                    datasetId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            //insert satement for storage_stability
            string storageStabilityInsert = "";

            for (int i = 0; i < newForm.PreActivationAges.Count; i++)
            {
                for (int j = 0; j < newForm.FormTreatments.Count; j++)
                {
                    for (int k = 0; k < newForm.DPAs.Count; k++)
                    {
                        for (int m = 0; m < newForm.Reps; m++) 
                        {
                            storageStabilityInsert += "insert into storage_stability (dataset_id, strain, form_trt, rep, preactivation_age, temp, dpa) values(@dataset_id, @strain, '" + newForm.FormTreatments[j] + "', '" + (m + 1) + "', '" + newForm.PreActivationAges[i] + "', @temp, '" + newForm.DPAs[k] + "');";
                        }
                    }
                }
            }

            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(storageStabilityInsert, conn);
                    cmd.Parameters.AddWithValue("@dataset_id", datasetId);
                    cmd.Parameters.AddWithValue("@strain", newForm.Strain);
                    cmd.Parameters.AddWithValue("@temp", newForm.TempdegreesCelsius);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return new int[] { datasetId, rowsAffected };
        }



        //Create Bead Stability Form
        public int[] CreateBeadStabilityForm(DateTime now, BeadStabilityForm bSForm)
        {
            int datasetID = 0;
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_AddDataset, conn);
                    cmd.Parameters.AddWithValue("@now", now);
                    cmd.Parameters.AddWithValue("@expType", "beads_stability");

                    datasetID = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            string beadStabilityInsert = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();

                    foreach (string t in bSForm.FormTreatments)
                    {
                        foreach (string b in bSForm.BeadAge)
                        {
                            for (int i = 1; i <= bSForm.TotalReps; i++)
                            {
                                for (int j = 0; j < bSForm.NumPerRep; j++)
                                {
                                    beadStabilityInsert += "INSERT INTO beads_stability (dataset_id, strain, form_trt, rep, bead_age) VALUES (@datasetid, @strain,'" + t + "', '" + i + "', '" + b + "');";
                                }
                            }
                        }
                    }
                    cmd = new SqlCommand(beadStabilityInsert, conn);
                    cmd.Parameters.AddWithValue("@datasetid", datasetID);
                    cmd.Parameters.AddWithValue("@strain", bSForm.Strain);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            ;
            return new int[] { datasetID, rowsAffected };
        }

        public int[] CreateChemicalCompatibilityForm(DateTime now, ChemCompatForm newForm)
        {
            int datasetId = 0;
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_AddDataset, conn);
                    cmd.Parameters.AddWithValue("@now", now);
                    cmd.Parameters.AddWithValue("@expType", "chem_compatibility");
                    datasetId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            //insert satement for chem_stability
            string storageStabilityInsert = "";

            for (int i = 0; i < newForm.Chemicals.Count; i++)
            {
                for (int j = 0; j < newForm.Reps; j++)
                {
                    for (int k = 0; k < newForm.RateDilutions.Count; k++)
                    {

                        storageStabilityInsert += "insert into chem_compatibility (dataset_id, strain, chemical, rep, rate_dilution) values(@dataset_id, @strain, '" + newForm.Chemicals[i] + "', '" + (j + 1) + "', '" + newForm.RateDilutions[k] + "');";

                    }
                }
            }

            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(storageStabilityInsert, conn);
                    cmd.Parameters.AddWithValue("@dataset_id", datasetId);
                    cmd.Parameters.AddWithValue("@strain", newForm.Strain);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return new int[] { datasetId, rowsAffected };
        }
        

        public List<DatasetInfo> GetDatasetInfo()
        {
            List<DatasetInfo> dataList = new List<DatasetInfo>();
            Helpers h = new Helpers();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_GetDatasetInfo, conn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dataList.Add(h.MakeDataset(r));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return dataList;
        }

        public bool UpdateDataSetInfo(List<DatasetInfo> data)
        {
            bool success = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();

                    foreach (DatasetInfo d in data)
                    {
                        cmd = new SqlCommand(SQL_UpdateDatasetInfo, conn);

                        if (d.PartnerName is null)
                            d.PartnerName = "";

                        if (d.FormulationDescription is null)
                            d.FormulationDescription = "";

                        if (d.Notes is null)
                            d.Notes = "";

                        cmd.Parameters.AddWithValue("@partnerName", d.PartnerName);
                        cmd.Parameters.AddWithValue("@formulationDescription", d.FormulationDescription);
                        cmd.Parameters.AddWithValue("@notes", d.Notes);
                        cmd.Parameters.AddWithValue("@experimentID", d.ExperimentID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        success = rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return success;

        }
    }
}