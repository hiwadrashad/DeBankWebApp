using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Interfaces;
using DeBank.Library.Models;
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
        IDataService _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();
        // GET: RegularUserOverview
        public ActionResult SubAccountsOverview()
        {
            //<summary>
            // mockingdata
            //<summary>

            //User mockuser = new User()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    dateofcreation = DateTime.Now,
            //    Name = "test",
            //    dummyaccount = true,
            //    Accounts = new List<BankAccount>()
            //    {
            //    new BankAccount()
            //    {
            //     Id = Guid.NewGuid().ToString(),
            //     dateofcreation = DateTime.Now,
            //     dummyaccount = true,
            //     Name = "test",
            //    },
            //     new BankAccount()
            //    {
            //     Id = Guid.NewGuid().ToString(),
            //     dateofcreation = DateTime.Now,
            //     dummyaccount = true,
            //     Name = "test",
            //    }
            //    }
            //};

            //StaticResources.CurrentUser.currentuser = mockuser;

            return View(StaticResources.CurrentUser.currentuser.Accounts);
        }

        public ActionResult TransactionsAndSaldoOverview(string id)
        { 
            //<summary>
            // mockingdata
            //<summary>

            //User mockuser = new User()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    dateofcreation = DateTime.Now,
            //    Name = "test",
            //    dummyaccount = true,
            //    Accounts = new List<BankAccount>()
            //    {
            //    new BankAccount()
            //    {
            //     Id = Guid.NewGuid().ToString(),
            //     dateofcreation = DateTime.Now,
            //     dummyaccount = true,
            //     Name = "test",
            //    },
            //     new BankAccount()
            //    {
            //     Id = Guid.NewGuid().ToString(),
            //     dateofcreation = DateTime.Now,
            //     dummyaccount = true,
            //     Name = "test",
            //    }
            //    }
            //};

            return View(_dataService.ReturnBankAccount(id).PreviousTransactions);
        }
    }
}
