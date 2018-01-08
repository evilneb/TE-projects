using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ExperimentForms
{
    public class StorageStabilityForm
    {

        [Required(ErrorMessage = "*")]
        [MinLength(1)]
        public string Strain { get; set; }

        [Required]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> FormTreatments { get; set; }

        [Required(ErrorMessage = "*")]
        [Range (1, 100, ErrorMessage = "The value must be greater than 0")]
        public int Reps { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> PreActivationAges { get; set; }

        [Required(ErrorMessage = "*")]
        [MinLength(1)]
        public string TempdegreesCelsius { get; set; }

        [Required(ErrorMessage = "*")]
        [Custom3Bar(ErrorMessage = "*")]
        public List<string> DPAs { get; set; }

    }

}