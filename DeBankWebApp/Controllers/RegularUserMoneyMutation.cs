using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Models;
using DeBankWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DeBank.Library.Logic;
using System.Linq;

namespace DeBankWebApp.Controllers
{
    public class RegularUserMoneyMutation : Controller
    {
        DeBank.Library.DAL.MockingData _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();

        // GET: RegularUserMoneyMutation
        public ActionResult TransferMoney(string id)
        {
            DeBank.Library.Logic.Transaction transaction = new DeBank.Library.Logic.Transaction();
            transaction = new DeBank.Library.Logic.Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = _dataService.ReturnBankAccount(id)
            };
            return View(transaction);
        }

        // GET: RegularUserMoneyMutation/Details/5
        [HttpPost]
        public async Task<IActionResult> TransferMoney(Transaction transaction)
        {
            if (_dataService.ReturnAllBankAccounts().Where(a => a.Id == transaction.InteractedAccount.Id).Any())
            {
                transaction.InteractedAccount = _dataService.ReturnAllBankAccounts().Where(a => a.Id == transaction.InteractedAccount.Id).FirstOrDefault();
                BankLogic item = new BankLogic();
                await item.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
                return View();
            }
            else
            {
                GeneralMethods.ShowUserNotFoundMessage();
                return View();
            }
            BankLogic bank = new BankLogic();

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
