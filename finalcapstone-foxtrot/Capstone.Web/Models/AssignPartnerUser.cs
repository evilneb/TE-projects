using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using Capstone.Web.Models;

namespace Capstone.Web.Models
{
    public class AssignPartnerUser
    {
        private string user;
        private string partner;

        public string User { get => user; set => user = value; }
        public string Partner { get => partner; set => partner = value; }

        //Properties for <SelectList> text/values
        public string Text { get; set; }
        public string Value { get; set; }

        //User dropdown list
        public SelectList UserDropDownList { get; set; }

        //Partner dropdown list
        public SelectList PartnerDropDownList { get; set; }
    }
}