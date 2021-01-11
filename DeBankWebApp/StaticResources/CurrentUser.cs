using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.StaticResources
{
    public static class CurrentUser
    {
        public static User currentuser {get; set;}

        public static BankAccount CurrentBankAccount { get; set; }
    }
}
