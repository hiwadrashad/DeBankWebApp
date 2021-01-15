using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeBank.Library.Models
{
    public class Information
    {
        [Key]
        public string id { get; set; }
        [Display(Name = "First name")]
        public string Firstname { get; set; }
        public string Addition { get; set; }
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Display(Name = "Postal code")]
        public string Postalcode { get; set; }

        [Display(Name = "Street name")]
        public string Streetname { get; set; }

        [Display(Name = "Street number")]
        public string Streetnumber { get; set; }
        public string City { get; set; }
       
        [Display(Name = "Telephone number")]
        public string Telephonenumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email adress")]
        public string Emailadress { get; set; }
    }
}
