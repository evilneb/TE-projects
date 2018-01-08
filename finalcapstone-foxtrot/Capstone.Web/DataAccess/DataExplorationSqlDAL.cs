using Capstone.Web.Models.DataExplorationModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DataAccess
{
    public class DataExplorationSqlDAL : IDataExplorationSqlDAL
    {
        private readonly string connectionString;
        private const string SQL_getAllStorageStabilityDataSql = "SELECT * FROM storage_stability;";
        private const string SQL_GetAllBeadStabilityDataSql = "SELECT * FROM beads_stability;";
        private const string SQL_GetAllChemicalCompaitibilityDataSql = "SELECT * FROM chem_compatibility;";
        private const string SQL_SummaryStorageStabilityData = "select * from storage_stability WHERE strain=@strain AND dpa=@dpa AND dataset_id =@dataset_id ORDER BY form_trt, preactivation_age;";
        private const string SQL_GetAllChemicalCompatibilityDataSql = "SELECT * FROM chem_compatibility;";
        private const string SQL_getPartnerSS = "SELECT * FROM storage_stability JOIN datasets ON storage_stability.dataset_id = datasets.id WHERE partner = @partnerName;";
        private const string SQL_getPartnerBS = "SELECT * FROM beads_stability JOIN datasets ON beads_stability.dataset_id = datasets.id WHERE partner = @partnerName";
        private const string SQL_getPartnerCC = "SELECT * FROM chem_compatibility JOIN datasets ON chem_compatibility.dataset_id = datasets.id WHERE partner = @partnerName";
        private const string SQL_GetSummaryChemicalCompatibilityDataSql = "SELECT * FROM chem_compatibility WHERE strain = @strain AND rate_dilution = @ratedil AND dataset_id = @dataset_id AND (chemical = @chemical1 OR chemical = @chemical2);";

        Helpers h = new Helpers();

        public DataExplorationSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<StorageStabilityExperimentData> GetStorageStabilityData()
        {
            List<StorageStabilityExperimentData> allStorageStabilityData = new List<StorageStabilityExperimentData>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_getAllStorageStabilityDataSql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StorageStabilityExperimentData rowOfStorageStabilityData = new StorageStabilityExperimentData();
                        rowOfStorageStabilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfStorageStabilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfStorageStabilityData.FormTreatments = Convert.ToString(reader["form_trt"]);
                        rowOfStorageStabilityData.Temp = Convert.ToString(reader["temp"]);
                        rowOfStorageStabilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfStorageStabilityData.PreActivationAges = Convert.ToString(reader["preactivation_age"]);
                        rowOfStorageStabilityData.DPA = Convert.ToString(reader["dpa"]);
                        rowOfStorageStabilityData.Notes = Convert.ToString(reader["notes"]);
                        rowOfStorageStabilityData.Dil_Zero = Convert.ToString(reader["dil_zero"]);
                        rowOfStorageStabilityData.Dil_One = Convert.ToString(reader["dil_neg1"]);
                        rowOfStorageStabilityData.Dil_Two = Convert.ToString(reader["dil_neg2"]);
                        rowOfStorageStabilityData.Dil_Three = Convert.ToString(reader["dil_neg3"]);
                        rowOfStorageStabilityData.Dil_Four = Convert.ToString(reader["dil_neg4"]);
                        rowOfStorageStabilityData.Dil_Five = Convert.ToString(reader["dil_neg5"]);
                        rowOfStorageStabilityData.Dil_Six = Convert.ToString(reader["dil_neg6"]);
                        rowOfStorageStabilityData.Dil_Seven = Convert.ToString(reader["dil_neg7"]);
                        rowOfStorageStabilityData.Outlier = Convert.ToBoolean(reader["outlier"]);
                        allStorageStabilityData.Add(rowOfStorageStabilityData);
                    }

                    return allStorageStabilityData;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<BeadStabilityRowData> GetBeadStabilityData()
        {
            List<BeadStabilityRowData> allBeadStabilityData = new List<BeadStabilityRowData>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllBeadStabilityDataSql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BeadStabilityRowData rowOfBeadStabilityData = new BeadStabilityRowData();
                        rowOfBeadStabilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfBeadStabilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfBeadStabilityData.FormulationTitration = Convert.ToString(reader["form_trt"]);
                        rowOfBeadStabilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfBeadStabilityData.BeadAge = Convert.ToString(reader["notes"]);
                        rowOfBeadStabilityData.Dil_1HR_Zero = Convert.ToString(reader["dil_1HR_zero"]);
                        rowOfBeadStabilityData.Dil_1HR_One = Convert.ToString(reader["dil_1HR_neg1"]);
                        rowOfBeadStabilityData.Dil_1HR_Two = Convert.ToString(reader["dil_1HR_neg2"]);
                        rowOfBeadStabilityData.Dil_1HR_Three = Convert.ToString(reader["dil_1HR_neg3"]);
                        rowOfBeadStabilityData.Dil_1HR_Four = Convert.ToString(reader["dil_1HR_neg4"]);
                        rowOfBeadStabilityData.Dil_1HR_Five = Convert.ToString(reader["dil_1HR_neg5"]);
                        rowOfBeadStabilityData.Dil_1HR_Six = Convert.ToString(reader["dil_1HR_neg6"]);
                        rowOfBeadStabilityData.Dil_1HR_Seven = Convert.ToString(reader["dil_1HR_neg7"]);
                        rowOfBeadStabilityData.Dil_6HR_Zero = Convert.ToString(reader["dil_6HR_zero"]);
                        rowOfBeadStabilityData.Dil_6HR_One = Convert.ToString(reader["dil_6HR_neg1"]);
                        rowOfBeadStabilityData.Dil_6HR_Two = Convert.ToString(reader["dil_6HR_neg2"]);
                        rowOfBeadStabilityData.Dil_6HR_Three = Convert.ToString(reader["dil_6HR_neg3"]);
                        rowOfBeadStabilityData.Dil_6HR_Four = Convert.ToString(reader["dil_6HR_neg4"]);
                        rowOfBeadStabilityData.Dil_6HR_Five = Convert.ToString(reader["dil_6HR_neg5"]);
                        rowOfBeadStabilityData.Dil_6HR_Six = Convert.ToString(reader["dil_6HR_neg6"]);
                        rowOfBeadStabilityData.Dil_6HR_Seven = Convert.ToString(reader["dil_6HR_neg7"]);
                        rowOfBeadStabilityData.Dil_6HR_Eight = Convert.ToString(reader["dil_6HR_neg8"]);
                        rowOfBeadStabilityData.Dil_24HR_Zero = Convert.ToString(reader["dil_24HR_zero"]);
                        rowOfBeadStabilityData.Dil_24HR_One = Convert.ToString(reader["dil_24HR_neg1"]);
                        rowOfBeadStabilityData.Dil_24HR_Two = Convert.ToString(reader["dil_24HR_neg2"]);
                        rowOfBeadStabilityData.Dil_24HR_Three = Convert.ToString(reader["dil_24HR_neg3"]);
                        rowOfBeadStabilityData.Dil_24HR_Four = Convert.ToString(reader["dil_24HR_neg4"]);
                        rowOfBeadStabilityData.Dil_24HR_Five = Convert.ToString(reader["dil_24HR_neg5"]);
                        rowOfBeadStabilityData.Dil_24HR_Six = Convert.ToString(reader["dil_24HR_neg6"]);
                        rowOfBeadStabilityData.Dil_24HR_Seven = Convert.ToString(reader["dil_24HR_neg7"]);
                        rowOfBeadStabilityData.Outlier = Convert.ToBoolean(reader["outlier"]);
                        allBeadStabilityData.Add(rowOfBeadStabilityData);
                    }

                    return allBeadStabilityData;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<ChemicalCompatibility> GetChemicalCompatibilityData()
        {
            List<ChemicalCompatibility> allChemicalCompatibilityData = new List<ChemicalCompatibility>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllChemicalCompatibilityDataSql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ChemicalCompatibility rowOfChemicalCompatibilityData = new ChemicalCompatibility();
                        rowOfChemicalCompatibilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfChemicalCompatibilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfChemicalCompatibilityData.Chemical = Convert.ToString(reader["chemical"]);
                        rowOfChemicalCompatibilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfChemicalCompatibilityData.Rate_Dilution = Convert.ToString(reader["rate_dilution"]);
                        rowOfChemicalCompatibilityData.Notes = Convert.ToString(reader["notes"]);
                        rowOfChemicalCompatibilityData.Dil_Zero = Convert.ToString(reader["dil_zero"]);
                        rowOfChemicalCompatibilityData.Dil_One = Convert.ToString(reader["dil_neg1"]);
                        rowOfChemicalCompatibilityData.Dil_Two = Convert.ToString(reader["dil_neg2"]);
                        rowOfChemicalCompatibilityData.Dil_Three = Convert.ToString(reader["dil_neg3"]);
                        rowOfChemicalCompatibilityData.Dil_Four = Convert.ToString(reader["dil_neg4"]);
                        rowOfChemicalCompatibilityData.Dil_Five = Convert.ToString(reader["dil_neg5"]);
                        rowOfChemicalCompatibilityData.Dil_Six = Convert.ToString(reader["dil_neg6"]);
                        rowOfChemicalCompatibilityData.Dil_Seven = Convert.ToString(reader["dil_neg7"]);
                        rowOfChemicalCompatibilityData.Outlier = Convert.ToBoolean(reader["outlier"]);

                        allChemicalCompatibilityData.Add(rowOfChemicalCompatibilityData);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return allChemicalCompatibilityData;
        }

        public List<StorageStabilityExperimentData> GetSSforPartner(string partnerName)
        {
            List<StorageStabilityExperimentData> data = new List<StorageStabilityExperimentData>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_getPartnerSS, conn);
                    cmd.Parameters.AddWithValue("@partnerName", partnerName);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        data.Add(h.MakeSSExploreModel(r));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return data;
        }

        public List<BeadStabilityRowData> GetBSforPartner(string partnerName)
        {
            List<BeadStabilityRowData> data = new List<BeadStabilityRowData>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_getPartnerBS, conn);
                    cmd.Parameters.AddWithValue("@partnerName", partnerName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BeadStabilityRowData rowOfBeadStabilityData = new BeadStabilityRowData();
                        rowOfBeadStabilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfBeadStabilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfBeadStabilityData.FormulationTitration = Convert.ToString(reader["form_trt"]);
                        rowOfBeadStabilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfBeadStabilityData.BeadAge = Convert.ToString(reader["notes"]);
                        rowOfBeadStabilityData.Dil_1HR_Zero = Convert.ToString(reader["dil_1HR_zero"]);
                        rowOfBeadStabilityData.Dil_1HR_One = Convert.ToString(reader["dil_1HR_neg1"]);
                        rowOfBeadStabilityData.Dil_1HR_Two = Convert.ToString(reader["dil_1HR_neg2"]);
                        rowOfBeadStabilityData.Dil_1HR_Three = Convert.ToString(reader["dil_1HR_neg3"]);
                        rowOfBeadStabilityData.Dil_1HR_Four = Convert.ToString(reader["dil_1HR_neg4"]);
                        rowOfBeadStabilityData.Dil_1HR_Five = Convert.ToString(reader["dil_1HR_neg5"]);
                        rowOfBeadStabilityData.Dil_1HR_Six = Convert.ToString(reader["dil_1HR_neg6"]);
                        rowOfBeadStabilityData.Dil_1HR_Seven = Convert.ToString(reader["dil_1HR_neg7"]);
                        rowOfBeadStabilityData.Dil_6HR_Zero = Convert.ToString(reader["dil_6HR_zero"]);
                        rowOfBeadStabilityData.Dil_6HR_One = Convert.ToString(reader["dil_6HR_neg1"]);
                        rowOfBeadStabilityData.Dil_6HR_Two = Convert.ToString(reader["dil_6HR_neg2"]);
                        rowOfBeadStabilityData.Dil_6HR_Three = Convert.ToString(reader["dil_6HR_neg3"]);
                        rowOfBeadStabilityData.Dil_6HR_Four = Convert.ToString(reader["dil_6HR_neg4"]);
                        rowOfBeadStabilityData.Dil_6HR_Five = Convert.ToString(reader["dil_6HR_neg5"]);
                        rowOfBeadStabilityData.Dil_6HR_Six = Convert.ToString(reader["dil_6HR_neg6"]);
                        rowOfBeadStabilityData.Dil_6HR_Seven = Convert.ToString(reader["dil_6HR_neg7"]);
                        rowOfBeadStabilityData.Dil_6HR_Eight = Convert.ToString(reader["dil_6HR_neg8"]);
                        rowOfBeadStabilityData.Dil_24HR_Zero = Convert.ToString(reader["dil_24HR_zero"]);
                        rowOfBeadStabilityData.Dil_24HR_One = Convert.ToString(reader["dil_24HR_neg1"]);
                        rowOfBeadStabilityData.Dil_24HR_Two = Convert.ToString(reader["dil_24HR_neg2"]);
                        rowOfBeadStabilityData.Dil_24HR_Three = Convert.ToString(reader["dil_24HR_neg3"]);
                        rowOfBeadStabilityData.Dil_24HR_Four = Convert.ToString(reader["dil_24HR_neg4"]);
                        rowOfBeadStabilityData.Dil_24HR_Five = Convert.ToString(reader["dil_24HR_neg5"]);
                        rowOfBeadStabilityData.Dil_24HR_Six = Convert.ToString(reader["dil_24HR_neg6"]);
                        rowOfBeadStabilityData.Dil_24HR_Seven = Convert.ToString(reader["dil_24HR_neg7"]);
                        data.Add(rowOfBeadStabilityData);
                    }

                    return data;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<ChemicalCompatibility> GetCCforPartner(string partnerName)
        {
            List<ChemicalCompatibility> data = new List<ChemicalCompatibility>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_getPartnerCC, conn);
                    cmd.Parameters.AddWithValue("@partnerName", partnerName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ChemicalCompatibility rowOfChemicalCompatibilityData = new ChemicalCompatibility();
                        rowOfChemicalCompatibilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfChemicalCompatibilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfChemicalCompatibilityData.Chemical = Convert.ToString(reader["chemical"]);
                        rowOfChemicalCompatibilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfChemicalCompatibilityData.Rate_Dilution = Convert.ToString(reader["rate_dilution"]);
                        rowOfChemicalCompatibilityData.Dil_Zero = Convert.ToString(reader["dil_zero"]);
                        rowOfChemicalCompatibilityData.Dil_One = Convert.ToString(reader["dil_neg1"]);
                        rowOfChemicalCompatibilityData.Dil_Two = Convert.ToString(reader["dil_neg2"]);
                        rowOfChemicalCompatibilityData.Dil_Three = Convert.ToString(reader["dil_neg3"]);
                        rowOfChemicalCompatibilityData.Dil_Four = Convert.ToString(reader["dil_neg4"]);
                        rowOfChemicalCompatibilityData.Dil_Five = Convert.ToString(reader["dil_neg5"]);
                        rowOfChemicalCompatibilityData.Dil_Six = Convert.ToString(reader["dil_neg6"]);
                        rowOfChemicalCompatibilityData.Dil_Seven = Convert.ToString(reader["dil_neg7"]);

                        data.Add(rowOfChemicalCompatibilityData);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return data;
        }

        public List<StorageStabilityExperimentData> GetSummaryStorageStabilityData(string strain, string dpa, string dataset_id)
        {
            List<StorageStabilityExperimentData> summaryStorageStability = new List<StorageStabilityExperimentData>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SummaryStorageStabilityData, conn);
                    cmd.Parameters.AddWithValue("@strain", strain);
                    cmd.Parameters.AddWithValue("@dpa", dpa);
                    cmd.Parameters.AddWithValue("@dataset_id", dataset_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StorageStabilityExperimentData rowOfStorageStabilityData = new StorageStabilityExperimentData();
                        rowOfStorageStabilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfStorageStabilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfStorageStabilityData.FormTreatments = Convert.ToString(reader["form_trt"]);
                        rowOfStorageStabilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfStorageStabilityData.Temp = Convert.ToString(reader["temp"]);
                        rowOfStorageStabilityData.PreActivationAges = Convert.ToString(reader["preactivation_age"]);
                        rowOfStorageStabilityData.DPA = Convert.ToString(reader["dpa"]);
                        rowOfStorageStabilityData.Notes = Convert.ToString(reader["notes"]);
                        rowOfStorageStabilityData.Dil_Zero = Convert.ToString(reader["dil_zero"]);
                        rowOfStorageStabilityData.Dil_One = Convert.ToString(reader["dil_neg1"]);
                        rowOfStorageStabilityData.Dil_Two = Convert.ToString(reader["dil_neg2"]);
                        rowOfStorageStabilityData.Dil_Three = Convert.ToString(reader["dil_neg3"]);
                        rowOfStorageStabilityData.Dil_Four = Convert.ToString(reader["dil_neg4"]);
                        rowOfStorageStabilityData.Dil_Five = Convert.ToString(reader["dil_neg5"]);
                        rowOfStorageStabilityData.Dil_Six = Convert.ToString(reader["dil_neg6"]);
                        rowOfStorageStabilityData.Dil_Seven = Convert.ToString(reader["dil_neg7"]);
                        summaryStorageStability.Add(rowOfStorageStabilityData);
                    }

                    return summaryStorageStability;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<BeadStabilityRowData> GetSummaryBeadStabilityData(string strain, string dataset_id_1, string dataset_id_2, string hours, string formTreatment1, string formTreatment2)
        {
            List<BeadStabilityRowData> summaryBeadStabilityData = new List<BeadStabilityRowData>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string SQL_SummaryBeadStabilityData = "";
                    string columnDilName = "";
                    if (hours == "1")
                    {
                        SQL_SummaryBeadStabilityData = "SELECT dataset_id, strain, form_trt, rep, bead_age, dil_1HR_zero, dil_1HR_neg1, dil_1HR_neg2, dil_1HR_neg3, dil_1HR_neg4, dil_1HR_neg5, dil_1HR_neg6, dil_1HR_neg7 From beads_stability WHERE strain = @strain AND dataset_id = @dataset_id_1 OR dataset_id = @dataset_id_2 AND form_trt = @formtreatment1 OR form_trt = @formtreatment2;";
                        columnDilName = "dil_1HR_";
                    }
                    else if (hours == "6")
                    {
                        SQL_SummaryBeadStabilityData = "SELECT dataset_id, strain, form_trt, rep, bead_age, dil_6HR_zero, dil_6HR_neg1, dil_6HR_neg2, dil_6HR_neg3, dil_6HR_neg4, dil_6HR_neg5, dil_6HR_neg6, dil_6HR_neg7 From beads_stability WHERE strain = @strain AND dataset_id = @dataset_id_1 OR dataset_id = @dataset_id_2 AND form_trt = @formtreatment1 OR form_trt = @formtreatment2;";
                        columnDilName = "dil_6HR_";
                    }
                    else if (hours == "24")
                    {
                        SQL_SummaryBeadStabilityData = "SELECT dataset_id, strain, form_trt, rep, bead_age, dil_24HR_zero, dil_24HR_neg1, dil_24HR_neg2, dil_24HR_neg3, dil_24HR_neg4, dil_24HR_neg5, dil_24HR_neg6, dil_24HR_neg7 From beads_stability WHERE strain = @strain AND dataset_id = @dataset_id_1 OR dataset_id = @dataset_id_2 AND form_trt = @formtreatment1 OR form_trt = @formtreatment2;";
                        columnDilName = "dil_24HR_";
                    }
                    SqlCommand cmd = new SqlCommand(SQL_SummaryBeadStabilityData, conn);
                    cmd.Parameters.AddWithValue("@strain", strain);
                    cmd.Parameters.AddWithValue("@dataset_id_1", dataset_id_1);
                    cmd.Parameters.AddWithValue("@dataset_id_2", dataset_id_2);
                    cmd.Parameters.AddWithValue("@formtreatment1", formTreatment1);
                    cmd.Parameters.AddWithValue("@formtreatment2", formTreatment2);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BeadStabilityRowData rowOfBeadStabilityData = new BeadStabilityRowData();
                        rowOfBeadStabilityData.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfBeadStabilityData.Strain = Convert.ToString(reader["strain"]);
                        rowOfBeadStabilityData.FormulationTitration = Convert.ToString(reader["form_trt"]);
                        rowOfBeadStabilityData.Rep = Convert.ToString(reader["rep"]);
                        rowOfBeadStabilityData.BeadAge = Convert.ToString(reader["bead_age"]);
                        rowOfBeadStabilityData.Dil_1HR_Zero = Convert.ToString(reader[columnDilName + "zero"]);
                        rowOfBeadStabilityData.Dil_1HR_One = Convert.ToString(reader[columnDilName + "neg1"]);
                        rowOfBeadStabilityData.Dil_1HR_Two = Convert.ToString(reader[columnDilName + "neg2"]);
                        rowOfBeadStabilityData.Dil_1HR_Three = Convert.ToString(reader[columnDilName + "neg3"]);
                        rowOfBeadStabilityData.Dil_1HR_Four = Convert.ToString(reader[columnDilName + "neg4"]);
                        rowOfBeadStabilityData.Dil_1HR_Five = Convert.ToString(reader[columnDilName + "neg5"]);
                        rowOfBeadStabilityData.Dil_1HR_Six = Convert.ToString(reader[columnDilName + "neg6"]);
                        rowOfBeadStabilityData.Dil_1HR_Seven = Convert.ToString(reader[columnDilName + "neg7"]);
                        summaryBeadStabilityData.Add(rowOfBeadStabilityData);
                    }

                    return summaryBeadStabilityData;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public string GetPartner(string username)
        {
            string partnerName = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select partner from partner_users where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {

                        partnerName = results["partner"].ToString();

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }


            return partnerName;
        }


        public List<ChemicalCompatibility> GetSummaryChemicalCompatibilityData(string strain, string dataset_id, string chemical1, string chemical2, string rate_dil)
        {
            List<ChemicalCompatibility> summaryChemicalCompatibility = new List<ChemicalCompatibility>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSummaryChemicalCompatibilityDataSql, conn);
                    cmd.Parameters.AddWithValue("@strain", strain);
                    cmd.Parameters.AddWithValue("@chemical1", chemical1);
                    cmd.Parameters.AddWithValue("@chemical2", chemical2);
                    cmd.Parameters.AddWithValue("@ratedil", rate_dil);
                    cmd.Parameters.AddWithValue("@dataset_id", dataset_id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ChemicalCompatibility rowOfChemicalCompatibility = new ChemicalCompatibility();
                        rowOfChemicalCompatibility.Dataset_ID = Convert.ToString(reader["dataset_id"]);
                        rowOfChemicalCompatibility.Strain = Convert.ToString(reader["strain"]);
                        rowOfChemicalCompatibility.Chemical = Convert.ToString(reader["chemical"]);
                        rowOfChemicalCompatibility.Rep = Convert.ToString(reader["rep"]);
                        rowOfChemicalCompatibility.Rate_Dilution = Convert.ToString(reader["rate_dilution"]);
                        rowOfChemicalCompatibility.Dil_Zero = Convert.ToString(reader["dil_zero"]);
                        rowOfChemicalCompatibility.Dil_One = Convert.ToString(reader["dil_neg1"]);
                        rowOfChemicalCompatibility.Dil_Two = Convert.ToString(reader["dil_neg2"]);
                        rowOfChemicalCompatibility.Dil_Three = Convert.ToString(reader["dil_neg3"]);
                        rowOfChemicalCompatibility.Dil_Four = Convert.ToString(reader["dil_neg4"]);
                        rowOfChemicalCompatibility.Dil_Five = Convert.ToString(reader["dil_neg5"]);
                        rowOfChemicalCompatibility.Dil_Six = Convert.ToString(reader["dil_neg6"]);
                        rowOfChemicalCompatibility.Dil_Seven = Convert.ToString(reader["dil_neg7"]);
                        summaryChemicalCompatibility.Add(rowOfChemicalCompatibility);
                    }

                    return summaryChemicalCompatibility;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

    }
}