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
        IDataService _dataService = MockingData.GetMockDataService();

        // GET: RegularUserMoneyMutation
        public ActionResult TransferMoney(string id)
        {
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                dummytransaction = false,
                Account = _dataService.ReturnBankAccount(id)
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
        public ActionResult Pay(string id)
        {
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = _dataService.ReturnBankAccount(id),
                dummytransaction = false,
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
