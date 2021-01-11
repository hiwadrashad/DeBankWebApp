using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DeBankWebApp.ViewModels
{
    public class TransactionViewModel
    {

        public DeBank.Library.Logic.Transaction transaction {get;set;}

        [Display(Name = "Account to send to")]
        public BankAccount sendaccount { get; set; }

    }
}
