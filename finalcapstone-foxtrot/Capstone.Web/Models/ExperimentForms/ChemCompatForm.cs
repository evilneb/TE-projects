using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ExperimentForms
{
    public class ChemCompatForm
    {
        [Required(ErrorMessage = "*")]
        [MinLength(1)]
        public string Strain { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> Chemicals { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Reps { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> RateDilutions { get; set; }
    }
}