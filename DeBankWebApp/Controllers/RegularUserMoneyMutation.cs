using DeBank.Library.DAL;
using DeBankWebApp.Data;
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
            Transaction transaction = new Transaction();
            transaction = new Transaction()
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
            BankLogic bank = WebBankLogic.GetBankLogic();

            await bank.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
            return View();
        }


        // GET: RegularUserMoneyMutation/Edit/5
        public ActionResult Pay()
        {
            Transaction transaction = new Transaction();
            transaction = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = StaticResources.CurrentUser.CurrentBankAccount,
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
