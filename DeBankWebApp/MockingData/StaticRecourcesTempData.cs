using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.MockingData
{
    public static class StaticRecourcesTempData
    {
        public static bool usemockdata = true;
        public static void AssignsValueStaticRecources(bool usemockdata)
        {
            if (usemockdata == true)
            {
                if (StaticResources.CurrentUser.currentuser == null)
                {
                    User mockuser = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        dateofcreation = DateTime.Now,
                        Name = "test",
                        dummyaccount = true,
                        Accounts = new List<BankAccount>()
                {
                new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 dateofcreation = DateTime.Now,
                 dummyaccount = true,
                 Name = "test",
                },
                 new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 dateofcreation = DateTime.Now,
                 dummyaccount = true,
                 Name = "test",
                }
                }
                    };
                    StaticResources.CurrentUser.currentuser = mockuser;
                    StaticResources.CurrentUser.CurrentBankAccount = mockuser.Accounts.FirstOrDefault();
                }
            }
        }
    }
}
