
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.Web.Dal
{
    public class ParkWeatherSqlDal : IParkWeatherDal
    {
       private readonly string connectionString;
        private string getParks = "select * from park";
        private string getForecasts = "select * from weather where parkCode = @parkCode order by fiveDayForecastValue asc";
        private string mostPopularBySurvey = "select * from park where parkCode = (select top 1 parkCode from survey_result group by parkCode order by COUNT(parkCode) desc)";
        private string saveSurvey = "insert into survey_result (parkCode, emailAddress, state, activityLevel) values(@parkCode, @email, @state, @activityLevel)";

        //Constructor
        public ParkWeatherSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Get a list of all the parks
        public List<Park> GetParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getParks, conn);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        parks.Add(CreatPark(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return parks;
        }

        //Get 5-day forecast for 1 park (in the form of a list)
        public List<DayForecast> GetForecasts(string parkCode)
        {
            List<DayForecast> forecasts = new List<DayForecast>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getForecasts, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader results = cmd.ExecuteReader();

                    while (results.Read())
                    {
                        forecasts.Add(CreateForecast(results));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return forecasts;
        }

        //Add new survey to the survey_result table
        public bool SaveSurvey(Survey newSurvey)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(saveSurvey, conn);
                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@email", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result = (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }

        //Get the most popular park
        public Park MostPopularBySurvey()
        {
            Park park = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(mostPopularBySurvey, conn);
                    SqlDataReader results = cmd.ExecuteReader();
                    while (results.Read())
                    {
                        park = CreatPark(results);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return park;
        }

        //helper methods
        private Park CreatPark(SqlDataReader results)
        {
            Park park = new Park();
            park.ParkCode = Convert.ToString(results["parkCode"]);
            park.Name = Convert.ToString(results["parkName"]);
            park.State = Convert.ToString(results["state"]);
            park.Acreage = Convert.ToString(results["acreage"]);
            park.Elevation = Convert.ToString(results["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToString(results["milesOfTrail"]);
            park.NumCampsites = Convert.ToString(results["numberOfCampsites"]);
            park.Climate = Convert.ToString(results["climate"]);
            park.YearFounded = Convert.ToString(results["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToString(results["annualVisitorCount"]);
            park.Quote = Convert.ToString(results["inspirationalQuote"]);
            park.QuoteSource = Convert.ToString(results["inspirationalQuoteSource"]);
            park.Description = Convert.ToString(results["parkDescription"]);
            park.EntryFee = Convert.ToString(results["entryFee"]);
            park.NumAnimalSpecies = Convert.ToString(results["numberOfAnimalSpecies"]);

            return park;
        }

        private DayForecast CreateForecast(SqlDataReader results)
        {
            DayForecast forecast = new DayForecast();
            forecast.ParkCode = Convert.ToString(results["parkCode"]);
            forecast.DaysOut = Convert.ToString(results["fiveDayForecastValue"]);
            forecast.Low = Convert.ToInt32(results["low"]);
            forecast.High = Convert.ToInt32(results["high"]);
            forecast.Forecast = Convert.ToString(results["forecast"]);

            return forecast;
        }
    }
}

