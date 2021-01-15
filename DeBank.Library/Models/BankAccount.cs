using DeBank.Library.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeBank.Library.Models
{
    public class BankAccount
    {
        [Key]
        public string Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public decimal Money { get; internal set; }
        [Display(Name = "Previous transactions")]
        public List<Transaction> PreviousTransactions = new List<Transaction>();
        public Information info {get;set;}
        public string IBAN { get; set; }
        [Display(Name = "Date of creation")]
        public DateTime DateOfCreation { get; set; }

        [NotMapped]
        public List<Transaction> TransactionQueue = new List<Transaction>();
        public event EventHandler<string> TransactionLog;

        public void Log(object sender, string line)
        {
            TransactionLog?.Invoke(this, line);
        }
    }
}
