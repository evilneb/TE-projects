using Capstone.Web.Models.DataEntryModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DataAccess
{
    public class DataEntrySqlDAL : IDataEntrySqlDAL
    {
        private readonly string connectionString;

        public DataEntrySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        //Get all Storage Stability Rows
        public List<DE_StorageStability> Storage_getAllRows()
        {
            List<DE_StorageStability> rowList = new List<DE_StorageStability>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from storage_stability join datasets on datasets.id = storage_stability.dataset_id", conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateStorageRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }

        //Get Storage Stability Rows by datasetId
        public List<DE_StorageStability> Storage_getDataset(int id)
        {
            List<DE_StorageStability> rowList = new List<DE_StorageStability>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from storage_stability join datasets on datasets.id = storage_stability.dataset_id where storage_stability.dataset_id = @datasetId", conn);
                    cmd.Parameters.AddWithValue("@datasetId", id);

                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateStorageRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }

        //Update a Storgae Stabilty Row
        public bool Update1Row(DE_StorageStability row)
        {
            int rowsUpdated = 0;
            //guard against null values
            if (row.Notes == null)
            {
                row.Notes = "";
            }
            for (int i = 0; i < row.DilutionResults.Count; i++)
            {
                if (row.DilutionResults[i] == null)
                {
                    row.DilutionResults[i] = "";
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update storage_stability set notes = @notes, dil_zero = @dil0, dil_neg1 = @dil1, dil_neg2 = @dil2, dil_neg3 = @dil3, dil_neg4 = @dil4, dil_neg5 = @dil5, dil_neg6 = @dil6, dil_neg7 = @dil7 where id = @rowId;", conn);
                    cmd.Parameters.AddWithValue("@rowId", row.Id);
                    cmd.Parameters.AddWithValue("@notes", row.Notes);
                    cmd.Parameters.AddWithValue("@dil0", row.DilutionResults[0]);
                    cmd.Parameters.AddWithValue("@dil1", row.DilutionResults[1]);
                    cmd.Parameters.AddWithValue("@dil2", row.DilutionResults[2]);
                    cmd.Parameters.AddWithValue("@dil3", row.DilutionResults[3]);
                    cmd.Parameters.AddWithValue("@dil4", row.DilutionResults[4]);
                    cmd.Parameters.AddWithValue("@dil5", row.DilutionResults[5]);
                    cmd.Parameters.AddWithValue("@dil6", row.DilutionResults[6]);
                    cmd.Parameters.AddWithValue("@dil7", row.DilutionResults[7]);


                    rowsUpdated = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }



            return rowsUpdated == 1;
        }

        //Storage Stability Helper method
        private DE_StorageStability CreateStorageRow(SqlDataReader results)
        {
            DE_StorageStability row = new DE_StorageStability();

            row.Id = Convert.ToInt32(results["id"]);
            row.CreatedDate = Convert.ToDateTime(results["created_date"]);
            row.DatadsetId = Convert.ToInt32(results["dataset_id"]);
            row.Strain = Convert.ToString(results["strain"]);
            row.FormTreatment = Convert.ToString(results["form_trt"]);
            row.Rep = Convert.ToInt32(results["rep"]);
            row.PreactivationAge = Convert.ToString(results["preactivation_age"]);
            row.Temp = Convert.ToString(results["temp"]);
            row.DPA = Convert.ToString(results["dpa"]);
            row.Notes = Convert.ToString(results["notes"]);

            row.DilutionResults = new List<string>();

            row.DilutionResults.Add(Convert.ToString(results["dil_zero"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg1"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg2"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg3"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_neg4"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg5"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg6"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg7"]));


            return row;
        }



        //Get all Bead Stability Rows
        public List<DE_BeadStability> Bead_getAllRows()
        {
            List<DE_BeadStability> rowList = new List<DE_BeadStability>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from beads_stability join datasets on datasets.id = beads_stability.dataset_id", conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateBeadRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }

        //Bead Stability
        public bool Update1Row(DE_BeadStability row)
        {
            int rowsUpdated = 0;
            if (row.Notes == null)
            {
                row.Notes = "";
            }
            for (int i = 0; i < row.DilutionResults.Count; i++)
            {
                if (row.DilutionResults[i] == null)
                {
                    row.DilutionResults[i] = "";
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update beads_stability set notes = @notes, dil_1HR_zero = @dil_1HR_zero, dil_1HR_neg1 = @dil_1HR_neg1, dil_1HR_neg2 = @dil_1HR_neg2, dil_1HR_neg3 = @dil_1HR_neg3, dil_1HR_neg4 = @dil_1HR_neg4, dil_1HR_neg5 = @dil_1HR_neg5, dil_1HR_neg6 = @dil_1HR_neg6, dil_1HR_neg7 = @dil_1HR_neg7, dil_6HR_zero = @dil_6HR_zero, dil_6HR_neg1 = @dil_6HR_neg1, dil_6HR_neg2 = @dil_6HR_neg2, dil_6HR_neg3 = @dil_6HR_neg3, dil_6HR_neg4 = @dil_6HR_neg4, dil_6HR_neg5 = @dil_6HR_neg5, dil_6HR_neg6 = @dil_6HR_neg6, dil_6HR_neg7 = @dil_6HR_neg7, dil_6HR_neg8 = @dil_6HR_neg8, dil_24HR_zero = @dil_24HR_zero, dil_24HR_neg1 = @dil_24HR_neg1, dil_24HR_neg2 = @dil_24HR_neg2, dil_24HR_neg3 = @dil_24HR_neg3, dil_24HR_neg4 = @dil_24HR_neg4, dil_24HR_neg5 = @dil_24HR_neg5, dil_24HR_neg6 = @dil_24HR_neg6, dil_24HR_neg7 = @dil_24HR_neg7 where id = @rowId;", conn);
                    cmd.Parameters.AddWithValue("@rowId", row.Id);
                    cmd.Parameters.AddWithValue("@notes", row.Notes);

                    cmd.Parameters.AddWithValue("@dil_1HR_zero", row.DilutionResults[0]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg1", row.DilutionResults[1]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg2", row.DilutionResults[2]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg3", row.DilutionResults[3]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg4", row.DilutionResults[4]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg5", row.DilutionResults[5]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg6", row.DilutionResults[6]);
                    cmd.Parameters.AddWithValue("@dil_1HR_neg7", row.DilutionResults[7]);

                    cmd.Parameters.AddWithValue("@dil_6HR_zero", row.DilutionResults[8]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg1", row.DilutionResults[9]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg2", row.DilutionResults[10]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg3", row.DilutionResults[11]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg4", row.DilutionResults[12]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg5", row.DilutionResults[13]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg6", row.DilutionResults[14]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg7", row.DilutionResults[15]);
                    cmd.Parameters.AddWithValue("@dil_6HR_neg8", row.DilutionResults[16]);

                    cmd.Parameters.AddWithValue("@dil_24HR_zero", row.DilutionResults[17]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg1", row.DilutionResults[18]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg2", row.DilutionResults[19]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg3", row.DilutionResults[20]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg4", row.DilutionResults[21]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg5", row.DilutionResults[22]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg6", row.DilutionResults[23]);
                    cmd.Parameters.AddWithValue("@dil_24HR_neg7", row.DilutionResults[24]);

                    rowsUpdated = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowsUpdated == 1;
        }

        //Bead Stability Helper Method
        private DE_BeadStability CreateBeadRow(SqlDataReader results)
        {
            DE_BeadStability row = new DE_BeadStability();

            row.Id = Convert.ToInt32(results["id"]);
            row.CreatedDate = Convert.ToDateTime(results["created_date"]);
            row.DatadsetId = Convert.ToInt32(results["dataset_id"]);
            row.Strain = Convert.ToString(results["strain"]);
            row.FormTreatment = Convert.ToString(results["form_trt"]);
            row.Rep = Convert.ToInt32(results["rep"]);
            row.BeadAge = Convert.ToString(results["bead_age"]);
            row.Notes = Convert.ToString(results["notes"]);

            row.DilutionResults = new List<string>();

            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_zero"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg1"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg2"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg3"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg4"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg5"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg6"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_1HR_neg7"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_zero"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg1"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg2"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg3"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg4"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg5"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg6"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg7"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_6HR_neg8"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_zero"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg1"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg2"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg3"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg4"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg5"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg6"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_24HR_neg7"]));
            return row;
        }



        //Chemical Compatibility
        public List<DE_ChemCompatibility> Chem_getAllRows()
        {
            List<DE_ChemCompatibility> rowList = new List<DE_ChemCompatibility>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from chem_compatibility join datasets on datasets.id = chem_compatibility.dataset_id", conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateChemRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }

        //Chemical Compatibility
        public bool Update1Row(DE_ChemCompatibility row)
        {
            int rowsUpdated = 0;
            //guard against null values
            if (row.Notes == null)
            {
                row.Notes = "";
            }
            for (int i = 0; i < row.DilutionResults.Count; i++)
            {
                if (row.DilutionResults[i] == null)
                {
                    row.DilutionResults[i] = "";
                }
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update chem_compatibility set notes = @notes, dil_zero = @dil0, dil_neg1 = @dil1, dil_neg2 = @dil2, dil_neg3 = @dil3, dil_neg4 = @dil4, dil_neg5 = @dil5, dil_neg6 = @dil6, dil_neg7 = @dil7 where id = @rowId;", conn);
                    cmd.Parameters.AddWithValue("@rowId", row.Id);
                    cmd.Parameters.AddWithValue("@notes", row.Notes);
                    cmd.Parameters.AddWithValue("@dil0", row.DilutionResults[0]);
                    cmd.Parameters.AddWithValue("@dil1", row.DilutionResults[1]);
                    cmd.Parameters.AddWithValue("@dil2", row.DilutionResults[2]);
                    cmd.Parameters.AddWithValue("@dil3", row.DilutionResults[3]);
                    cmd.Parameters.AddWithValue("@dil4", row.DilutionResults[4]);
                    cmd.Parameters.AddWithValue("@dil5", row.DilutionResults[5]);
                    cmd.Parameters.AddWithValue("@dil6", row.DilutionResults[6]);
                    cmd.Parameters.AddWithValue("@dil7", row.DilutionResults[7]);


                    rowsUpdated = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }



            return rowsUpdated == 1;
        }

        //Chemical Compatibility Helper method
        private DE_ChemCompatibility CreateChemRow(SqlDataReader results)
        {
            DE_ChemCompatibility row = new DE_ChemCompatibility();

            row.Id = Convert.ToInt32(results["id"]);
            row.CreatedDate = Convert.ToDateTime(results["created_date"]);
            row.DatadsetId = Convert.ToInt32(results["dataset_id"]);
            row.Strain = Convert.ToString(results["strain"]);
            row.Chemical = Convert.ToString(results["chemical"]);
            row.Rep = Convert.ToInt32(results["rep"]);
            row.RateDilution = Convert.ToString(results["rate_dilution"]);
            row.Notes = Convert.ToString(results["notes"]);

            row.DilutionResults = new List<string>();

            row.DilutionResults.Add(Convert.ToString(results["dil_zero"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg1"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg2"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg3"]));

            row.DilutionResults.Add(Convert.ToString(results["dil_neg4"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg5"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg6"]));
            row.DilutionResults.Add(Convert.ToString(results["dil_neg7"]));


            return row;
        }





        public string GetTableByID(int datasetId)
        {
            string exType = "";
            HashSet<int> ids = new HashSet<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select id from datasets", conn);

                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        ids.Add(Convert.ToInt32(results["id"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            
            if (ids.Contains(datasetId))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand("select experiment_type from datasets where id = @datasetId", conn);
                        cmd.Parameters.AddWithValue("@datasetId", datasetId);

                        SqlDataReader results = cmd.ExecuteReader();

                        while (results.Read())
                        {
                            exType = Convert.ToString(results["experiment_type"]);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }

                
            }
            return exType;
        }



        //Get Bead Stability Rows by datasetId
        public List<DE_BeadStability> Bead_getDataset(int id)
        {
            List<DE_BeadStability> rowList = new List<DE_BeadStability>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from beads_stability join datasets on datasets.id = beads_stability.dataset_id where beads_stability.dataset_id = @datasetId", conn);
                    cmd.Parameters.AddWithValue("@datasetId", id);

                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateBeadRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }


        //Get Chem Compatibility Rows by datasetId
        public List<DE_ChemCompatibility> Chem_getDataset(int id)
        {
            List<DE_ChemCompatibility> rowList = new List<DE_ChemCompatibility>();
            //insert statement for datasets
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from chem_compatibility join datasets on datasets.id = chem_compatibility.dataset_id where chem_compatibility.dataset_id = @datasetId", conn);
                    cmd.Parameters.AddWithValue("@datasetId", id);

                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        rowList.Add(CreateChemRow(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowList;
        }
    }
}