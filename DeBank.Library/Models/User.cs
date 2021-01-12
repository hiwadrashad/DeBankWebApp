using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeBank.Library.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string Name { get; set; }
        public Information info { get; set; }
        public bool dummyaccount { get; set; }
        public virtual List<BankAccount> Accounts { get; set; }

        [Display(Name = "Date of creation")]
        public DateTime dateofcreation { get; set; }
    }
}
