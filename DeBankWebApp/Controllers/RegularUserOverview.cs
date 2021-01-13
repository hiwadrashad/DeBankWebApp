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
        IDataService _dataService = DataService.GetDataService();
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

            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);

            return View(StaticResources.CurrentUser.currentuser.Accounts);
        }

        public ActionResult TransactionsAndSaldoOverview()
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

            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);
            IEnumerable<BankAccount> listedcurrentitem = GeneralMethods.CreateEnumerable<BankAccount>(StaticResources.CurrentUser.CurrentBankAccount);
            return View(listedcurrentitem);
        }
    }
}
