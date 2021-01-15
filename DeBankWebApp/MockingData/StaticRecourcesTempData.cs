using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.MockingData
{
    public static class StaticRecourcesTempData
    {
        public static bool usemockdata = false;
        public static void AssignsValueStaticRecources(bool usemockdata)
        {
            if (usemockdata == true)
            {
                if (StaticResources.CurrentUser.currentuser == null)
                {
                    User mockuser = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "test",
                        Accounts = new List<BankAccount>()
                {
                new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 Name = "test",
                 info = new Information()
                 {
                  Addition = "test",
                  City = "test",
                  Emailadress = "test@hotmail.com",
                  Firstname = "test",
                  Lastname = "test",
                  Postalcode = "test",
                  Streetname = "test",
                  Streetnumber = "test",
                  Telephonenumber = "test"
                 },
                },
                 new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 Name = "test",
                 info = new Information()
                 {
                  Addition = "test",
                  City = "test",
                  Emailadress = "test@hotmail.com",
                  Firstname = "test",
                  Lastname = "test",
                  Postalcode = "test",
                  Streetname = "test",
                  Streetnumber = "test",
                  Telephonenumber = "test"
                 },
                }
                }
                    };
                    mockuser.Accounts.FirstOrDefault().Owner = mockuser;
                    mockuser.Accounts.Skip(1).FirstOrDefault().Owner = mockuser;
                    StaticResources.CurrentUser.currentuser = mockuser;
                    StaticResources.CurrentUser.CurrentBankAccount = mockuser.Accounts.FirstOrDefault();
                }
            }
        }
    }
}
