using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class User
    {
        //variable
        private string username;
        private string password;
        private bool administrator;
        private bool researcher;
        private bool technician;
        private bool partneruser;
        
        //properties
        [Required]
        [MaxLength(20, ErrorMessage = "Max Length is 32 Characters)")]
        [MinLength(4, ErrorMessage = "Username must be 4 characters or more")]
        public string Username { get => username; set => username = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max Length is 32 Characters)")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or more")]
        public string Password { get => password; set => password = value; }
        public bool Administrator { get => administrator; set => administrator = value; }
        public bool Researcher { get => researcher; set => researcher = value; }
        public bool Technician { get => technician; set => technician = value; }
        public bool Partneruser { get => partneruser; set => partneruser = value; }
        
    }

}