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
using System.Net;
using System.IO;
using System.Text;

namespace DeBankWebApp.Controllers
{
    public class RegularUserMoneyMutation : Controller
    {
        IDataService _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();

        [Authorize]

        public IActionResult TransferMoney(string id)
        {
            Transaction transaction = new Transaction();
            StaticResources.CurrentUser.CurrentBankAccount = _dataService.ReturnBankAccount(id);
            return View(transaction);
        }

        [Authorize]
        public IActionResult SuccesfullTrade()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TransferMoney(Transaction transaction)
        {
            try
            {
                BankLogic bank = WebBankLogic.GetBankLogic();
                if (_dataService.ReturnAllBankAccounts().Where(a => a.Id == transaction.InteractedAccount.Id).Any())
                {
                    transaction.Id = Guid.NewGuid().ToString();
                    transaction.Account = StaticResources.CurrentUser.CurrentBankAccount;
                    transaction.InteractedAccount = _dataService.ReturnAllBankAccounts().Where(a => a.Id == transaction.InteractedAccount.Id).FirstOrDefault();
                    await bank.TransferMoney(transaction.Account, transaction.InteractedAccount, transaction.Amount);
                    return RedirectToAction("SuccesfullTrade", "RegularUserMoneyMutation");
                }
                else
                {
                    ViewBag.Message = "User was not found";
                    return View();
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                ViewBag.Message = "Wrong value input, please try a valid value";
                return View();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                ViewBag.Message = "Wrong value input, please try a valid value";
                return View();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                ViewBag.Message = "Something went wrong, try again";
                return View();
            }
        }


        [Authorize]

        public IActionResult Pay(string id)
        {
            Transaction transaction = new Transaction();
            StaticResources.CurrentUser.CurrentBankAccount = _dataService.ReturnBankAccount(id);
            return View(transaction);
        }

        [Authorize]

        public IActionResult PaypalTransfer()
        {
            //TempData.Put("carryoverkey", TempData.Get<Transaction>("key"));
            //IPN item = new IPN();
            //item.Receive();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pay(Transaction transaction)
        {
            try
            {
                if (IBAN.IBAN.ValidateIBAN(transaction.InteractedAccount.IBAN))
                {
                    transaction.Id = Guid.NewGuid().ToString();
                    transaction.Account = StaticResources.CurrentUser.CurrentBankAccount;
                    //TempData.Put("key", transaction);
                    return RedirectToAction("PaypalTransfer", "RegularUserMoneyMutation");
                }
                else
                {
                    ViewBag.Message = "User was not found";
                    return View();
                }
            //BankLogic.SpendMoney();
        }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                ViewBag.Message = "Wrong value input, please try a valid value";
                return View();
    }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                ViewBag.Message = "Wrong value input, please try a valid value";
                return View();
}
#pragma warning disable CS0168 // Variable is declared but never used
catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
{
    ViewBag.Message = "Something went wrong, try again";
    return View();
}
        }

    }
}
