using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
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
            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);
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
            if (_dataService.ReturnAllBankAccounts().Where(a => a.IBAN == transaction.InteractedAccount.IBAN).Any())
            {
                transaction.InteractedAccount = _dataService.ReturnAllBankAccounts().Where(a => a.IBAN == transaction.InteractedAccount.IBAN).FirstOrDefault();
                await DeBank.Library.Logic.BankLogic.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
                return View();
            }
            else
            {
                GeneralMethods.ShowUserNotFoundMessage();
                return View();
            }
        }


        // GET: RegularUserMoneyMutation/Edit/5
        public ActionResult Pay()
        {
            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = StaticResources.CurrentUser.CurrentBankAccount,
                dummytransaction = false,
            };
            return View(transaction);
        }

        // POST: RegularUserMoneyMutation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(DeBank.Library.Logic.Transaction transaction)
        {
            try
            {
                if (IBAN.IBAN.ValidateIBAN(transaction.InteractedAccount.IBAN))
                { 
                
                }
                //DeBank.Library.Logic.BankLogic.SpendMoney();
                return RedirectToAction("TransactionsAndSaldoOverview", "RegularUserOverview");
            }
            catch
            {
                return View();
            }
        }

    }
}
