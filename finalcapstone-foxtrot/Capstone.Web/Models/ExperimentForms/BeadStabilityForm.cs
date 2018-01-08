using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ExperimentForms
{
    public class BeadStabilityForm
    {
        [Required(ErrorMessage = "*")]
        public string Strain { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> FormTreatments { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int TotalReps { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int NumPerRep { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> BeadAge { get; set; }

    }
}