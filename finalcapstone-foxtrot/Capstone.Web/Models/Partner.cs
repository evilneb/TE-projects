using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Partner
    {
        //variable
        private string partnerName;

        //properties
        [Required]
        [MinLength(2, ErrorMessage = "Partner Name must be 2 characters or more")]
        public string PartnerName { get => partnerName; set => partnerName = value; }

        //methods

    }
}