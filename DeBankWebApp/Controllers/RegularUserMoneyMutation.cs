using DeBank.Library.DAL;
using DeBank.Library.Interfaces;
using DeBank.Library.Models;
using DeBankWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeBankWebApp.Controllers
{
    public class RegularUserMoneyMutation : Controller
    {
        IDataService _dataService = DataService.GetDataService();

        // GET: RegularUserMoneyMutation
        public ActionResult TransferMoney()
        {
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
             Id = Guid.NewGuid().ToString(),
             dummytransaction = false,
             Account = StaticResources.CurrentUser.CurrentBankAccount
            };
            return View(transaction);
        }

        // GET: RegularUserMoneyMutation/Details/5
        [HttpPost]
        public async Task<IActionResult> TransferMoney(DeBank.Library.Logic.Transaction transaction)
        {
            await DeBank.Library.Logic.BankLogic.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
            return View();
        }


        // GET: RegularUserMoneyMutation/Edit/5
        public ActionResult Pay()
        {
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = StaticResources.CurrentUser.CurrentBankAccount,
                dummytransaction = false,
            };
            return View();
        }

        // POST: RegularUserMoneyMutation/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Pay()
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
