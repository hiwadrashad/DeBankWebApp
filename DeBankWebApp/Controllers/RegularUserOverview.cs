using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Models;
using DeBankWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DeBankWebApp.Controllers
{
    public class RegularUserOverview : Controller
    {
        IDataService _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();
        // GET: RegularUserOverview
        [Authorize]
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

   
        [Authorize]
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
            if (_dataService.ReturnBankAccount(id).PreviousTransactions == null)
            {
                _dataService.ReturnBankAccount(id).PreviousTransactions = new List<DeBank.Library.Logic.Transaction>();
            }
            return View(_dataService.ReturnBankAccount(id));
        }
    }
}
