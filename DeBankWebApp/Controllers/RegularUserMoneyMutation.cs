using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Models;
using DeBankWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;
using DeBank.Library.Logic;
using System.Linq;
using DeBankWebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace DeBankWebApp.Controllers
{
    public class RegularUserMoneyMutation : Controller
    {
        IDataService _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();

        // GET: RegularUserMoneyMutation
        [Authorize]

        public ActionResult TransferMoney(string id)
        {
            Transaction transaction = new Transaction();
            transaction = new Transaction()
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
            BankLogic bank = WebBankLogic.GetBankLogic();
            if (_dataService.ReturnAllBankAccounts().Where(a => a.IBAN == transaction.InteractedAccount.IBAN).Any())
            {
                transaction.InteractedAccount = _dataService.ReturnAllBankAccounts().Where(a => a.IBAN == transaction.InteractedAccount.IBAN).FirstOrDefault();
                await bank.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
                return View();
            }
            else
            {
                GeneralMethods.ShowUserNotFoundMessage();
                return View();
            }
        }


        // GET: RegularUserMoneyMutation/Edit/5
        [Authorize]

        public ActionResult Pay(string id)
        {
            Transaction transaction = new Transaction();
            transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = _dataService.ReturnBankAccount(id),
            };
            return View(transaction);
        }

        [Authorize]

        public ActionResult PaypalTransfer()
        {
            TempData["IBANcarryover"] = TempData["IBAN"];
            return View();
        }

        // POST: RegularUserMoneyMutation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(Transaction transaction)
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
                //BankLogic.SpendMoney();
            }
            catch
            {
                return View();
            }
        }

    }
}
