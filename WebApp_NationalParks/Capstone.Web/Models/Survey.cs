using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Survey
    {
        public string ParkCode { get; set; }

        //add validation tags to email field
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string State { get; set; }

        public string ActivityLevel { get; set; }

        public static List<SelectListItem> ActivityLevels { get; } = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Inactive", Value = "Inactive" },
            new SelectListItem() { Text = "Sedentary", Value = "Sedentary" },
            new SelectListItem() { Text = "Active", Value = "Active" },
            new SelectListItem() { Text = "Extremely Active", Value = "Extremely Active" }
        };
    }
}