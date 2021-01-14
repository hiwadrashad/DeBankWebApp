using DeBank.Library.DAL;
using DeBank.Library.Models;
using DeBankWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.Controllers
{
    public class RegularUserOverview : Controller
    {
        IDataService _dataService = DataService.GetDataService();
        // GET: RegularUserOverview
        public ActionResult SubAccountsOverview()
        {
            //<summary>
            // mockingdata
            //<summary>

            User mockuser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                DateOfCreation = DateTime.Now,
                Name = "test",
                Accounts = new List<BankAccount>()
                {
                new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 DateOfCreation = DateTime.Now,
                 Name = "test 1",
                },
                 new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 DateOfCreation = DateTime.Now,
                 Name = "test 2",
                }
                }
            };

            StaticResources.CurrentUser.currentuser = mockuser;
            return View(StaticResources.CurrentUser.currentuser.Accounts);
        }

        public ActionResult TransactionsAndSaldoOverview()
        { 
            //<summary>
            // mockingdata
            //<summary>

            User mockuser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                DateOfCreation = DateTime.Now,
                Name = "test",
                Accounts = new List<BankAccount>()
                {
                new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 DateOfCreation = DateTime.Now,
                 Name = "test 1",
                },
                 new BankAccount()
                {
                 Id = Guid.NewGuid().ToString(),
                 DateOfCreation = DateTime.Now,
                 Name = "test 2",
                }
                }
            };

            StaticResources.CurrentUser.currentuser = mockuser;
            StaticResources.CurrentUser.CurrentBankAccount = mockuser.Accounts.FirstOrDefault();
            return View(StaticResources.CurrentUser.CurrentBankAccount.PreviousTransactions);
        }
    }
}
