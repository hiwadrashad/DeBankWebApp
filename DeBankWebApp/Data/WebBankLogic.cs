using DeBank.Library.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.Data
{
    public class WebBankLogic
    {
        private static BankLogic _logic;

        private WebBankLogic()
        {
        }

        public static BankLogic GetBankLogic()
        {
            if (_logic == null)
            {
                _logic = new BankLogic();
            }
            return _logic;
        }
    }
}
