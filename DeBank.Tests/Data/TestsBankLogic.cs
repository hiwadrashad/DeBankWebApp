using DeBank.Library.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBank.Tests.Data
{
    public class TestsBankLogic
    {
        private static BankLogic _logic;

        private TestsBankLogic()
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
