using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Interfaces;
using DeBank.Library.Models;
using DeBankWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DeBank.Library.Logic;

namespace DeBankWebApp.Controllers
{
    public class RegularUserMoneyMutation : Controller
    {
        IDataService _dataService = DataService.GetDataService();

        // GET: RegularUserMoneyMutation
        public ActionResult TransferMoney()
        {
            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);
            Transaction transaction = new Transaction()
            {
             Id = Guid.NewGuid().ToString(),
             Account = StaticResources.CurrentUser.CurrentBankAccount
            };
            return View(transaction);
        }

        // GET: RegularUserMoneyMutation/Details/5
        [HttpPost]
        public async Task<IActionResult> TransferMoney(Transaction transaction)
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
            BankLogic bank = WebBankLogic.GetBankLogic();

            await bank.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
            return View();
        }


        // GET: RegularUserMoneyMutation/Edit/5
        public ActionResult Pay()
        {
            MockingData.StaticRecourcesTempData.AssignsValueStaticRecources(MockingData.StaticRecourcesTempData.usemockdata);
            Transaction transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = StaticResources.CurrentUser.CurrentBankAccount,
            };
            return View(transaction);
        }

        public ActionResult PaypalTransfer()
        {
            TempData["IBANcarryover"] = TempData["IBAN"];
            return View();
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
                    TempData["IBAN"] = transaction.InteractedAccount.IBAN;
                    return RedirectToAction("PaypalTransfer", "RegularUserMoneyMutation");
                }
                else
                {
                    GeneralMethods.ShowUserNotFoundMessage();
                    return View();
                }
                //DeBank.Library.Logic.BankLogic.SpendMoney();
            }
            catch
            {
                return View();
            }
        }

    }
}
