using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeBank.Library.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Naam")]
        public string Name { get; set; }
        public Information Info { get; set; }
        public virtual List<BankAccount> Accounts { get; set; }

        [Display(Name = "Date of creation")]
        public DateTime DateOfCreation { get; set; }
    }
}
