using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DataAccess
{
    public class UserSqlDAL : IUserSqlDAL
    {

        private readonly string connectionString;

        //constructor where the object gets the database connection
        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Create user
        public bool SaveUser(User user)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO users VALUES (@username, @password); insert into roles values(@username, @tech, @researcher, @partner, @admin)", conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@admin", user.Administrator);
                    cmd.Parameters.AddWithValue("@researcher", user.Researcher);
                    cmd.Parameters.AddWithValue("@tech", user.Technician);
                    cmd.Parameters.AddWithValue("@partner", user.Partneruser);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return rowsAffected > 0;
        }

        //Check whether a username is stored in the database
        public bool UsernameExists(string username)
        {
            int result = 0;

            if (username != null && username.Length > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd;
                        conn.Open();
                        cmd = new SqlCommand("Select COUNT(username) from users where username = @username;", conn);
                        cmd.Parameters.AddWithValue("@username", username);

                        result = (int)cmd.ExecuteScalar();
                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }
            }
            return result == 1;
        }

        //Check whether a password is correct for a specific user
        public bool PasswordIsCorrect(string username, string password)
        {
            string comparePassword = "";
            if ((username != null && username.Length > 0) && (password != null && password.Length > 0))
            {


                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd;
                        conn.Open();
                        cmd = new SqlCommand("Select password from users where username = @username; ", conn);
                        cmd.Parameters.AddWithValue("@username", username);
                        comparePassword = (string)cmd.ExecuteScalar();

                    }
                }
                catch (SqlException ex)
                {
                    throw;
                }
                return password.Equals(comparePassword);
            }

            return false;
        }

        //Methods to check roles
        public bool IsAdmin(string username)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("Select administrator from roles where username = @username; ", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    result = (bool)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public bool IsResearcher(string username)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("Select researcher from roles where username = @username; ", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    result = (bool)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public bool IsTechnician(string username)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("Select technician from roles where username = @username; ", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    result = (bool)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        public bool IsPartner(string username)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("Select partner from roles where username = @username; ", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    result = (bool)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }


        //Remove role designation from a user
        public bool RemoveAdminRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set administrator = 0 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool RemoveResearchRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set researcher = 0 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool RemoveTechRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set technician = 0 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool RemovePartnerRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set partner = 0 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        //Add role for an existing a user
        public bool AddAdminRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set administrator = 1 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool AddResearchRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set researcher = 1 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool AddTechRole(string username)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set technician = 1 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result == 1;
        }

        public bool AddPartnerRole(string username)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update roles set partner = 1 where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result == 1;
        }

        //Change a user's password
        //ONLY FOR AN ADMINISTRATOR to access in order to lock someone out of the system
        //We could make it so that a user can change their password but then you'd need to use the 
        //PasswordIsCorrect method above first and not let them change it if it returns false
        public bool ChangeUsersPassword(string username, string newPassword)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd;
                    conn.Open();
                    cmd = new SqlCommand("update users set password = @newPassword where username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return result == 1;
        }

    }
}