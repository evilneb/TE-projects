using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Capstone.Web.DataAccess
{
    public class PartnerSqlDAL : IPartnerSqlDAL
    {
        private readonly string connectionString;
        private const string SQL_AssignPartnerUser = "INSERT INTO partner_users (partner, username) SELECT partner, @username FROM partners WHERE partner = @partner; ";
        private const string SQL_PartnerExists = "Select COUNT(partner) from partners where partner = @partner;";
        private const string SQL_AddPartner = "INSERT INTO partners VALUES (@partner);";
        private const string SQL_GetUserList = "SELECT username FROM roles WHERE partner = '1';";
        private const string SQL_GetPartnerList = "SELECT partner FROM partners;";

        //constructor where object gets the database connection
        public PartnerSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //create partner
        public bool AddPartner(Partner partner)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_AddPartner, conn);
                    cmd.Parameters.AddWithValue("@partner", partner.PartnerName);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowsAffected > 0;
        }

        //Check whether a partner is stored in the database
        public bool PartnerExists(string partner)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_PartnerExists, conn);
                    cmd.Parameters.AddWithValue("@partner", partner);

                    result = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        //assign user to partner
        public bool AssignPartnerUser(AssignPartnerUser partnerUser)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand(SQL_AssignPartnerUser, conn);
                    cmd.Parameters.AddWithValue("@partner", partnerUser.Partner);
                    cmd.Parameters.AddWithValue("@username", partnerUser.User);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowsAffected > 0;
        }

        //to use with SelectList UserDropDownList
        public SelectList GetUserList()
        {
            AssignPartnerUser assignPartnerUser = new AssignPartnerUser();
            List<AssignPartnerUser> userList = new List<AssignPartnerUser>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetUserList, conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        userList.Add(new AssignPartnerUser
                        {
                            Text = Convert.ToString(results["username"]),
                            Value = Convert.ToString(results["username"])
                        });
                    }
                }
                assignPartnerUser.UserDropDownList = new SelectList(userList, "Value", "Text");
            }
            catch (SqlException ex)
            {
                throw;
            }
            return assignPartnerUser.UserDropDownList;
        }

        //to use with SelectList PartnerDropDownList
        public SelectList GetPartnerList()
        {
            AssignPartnerUser assignPartnerUser = new AssignPartnerUser();
            List<AssignPartnerUser> partnerList = new List<AssignPartnerUser>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetPartnerList, conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        partnerList.Add(new AssignPartnerUser
                        {
                            Text = Convert.ToString(results["partner"]),
                            Value = Convert.ToString(results["partner"])
                        });
                    }
                }
                assignPartnerUser.PartnerDropDownList = new SelectList(partnerList, "Value", "Text");
            }
            catch (SqlException ex)
            {
                throw;
            }
            return assignPartnerUser.PartnerDropDownList;
        }

        ////NOT PART OF MVP: for admin to remove username - partner association
        //public bool RemovePartner(string username, string partner)
        //{
        //    int result = 0;
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd;
        //            conn.Open();
        //            cmd = new SqlCommand("delete from partners where username = @username and partner = @partner", conn);
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@partner", partner);

        //            result = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }

        //    return result == 1;
        //}

        
        
    }
}