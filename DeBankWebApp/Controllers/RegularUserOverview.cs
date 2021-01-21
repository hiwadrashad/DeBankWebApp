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
        [Authorize]
        public ActionResult SubAccountsOverview()
        {

            return View(StaticResources.CurrentUser.currentuser.Accounts);
        }

   
        [Authorize]
        public IActionResult TransactionsAndSaldoOverview(string id)
        {

            if (_dataService.ReturnBankAccount(id).PreviousTransactions == null)
            {
                _dataService.ReturnBankAccount(id).PreviousTransactions = new List<DeBank.Library.Logic.Transaction>();
            }
            return View(_dataService.ReturnBankAccount(id));
        }
    }
}
