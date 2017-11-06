using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.Dal
{
    public interface IParkWeatherDal
    {
        List<Park> GetParks();

        List<DayForecast> GetForecasts(string parkCode);

        bool SaveSurvey(Survey newSurvey);

        Park MostPopularBySurvey();
    }
}
