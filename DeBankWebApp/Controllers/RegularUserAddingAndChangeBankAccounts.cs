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

namespace DeBankWebApp.Controllers
{
    public class RegularUserAddingAndChangeBankAccounts : Controller
    {
        DeBank.Library.DAL.MockingData _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();
        // GET: RegularUserAddingBankAccounts/Create
        [Authorize]
        public ActionResult GenerateBankAccount()
        {
            BankAccount account = new BankAccount();
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    DateOfCreation = DateTime.Now,
                //IBAN = IBAN.IBAN.GenerateIBANNumber(),
            //    Owner = StaticResources.CurrentUser.currentuser,
            //};
            return View(account);
        }

        // POST: RegularUserAddingBankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBankAccount(BankAccount account)
        {
            try
            {
                var item = StaticResources.CurrentUser.currentuser;
                account.Id = Guid.NewGuid().ToString();
                account.DateOfCreation = DateTime.Now;
                account.Owner = StaticResources.CurrentUser.currentuser;
                if (item.Accounts == null)
                {
                    item.Accounts = new List<BankAccount>();
                }
                item.Accounts.Add(account);
                _dataService.UpdateUser(item);
                _dataService.AddBankaccounts(account);
                return RedirectToAction("SubAccountsOverview", "RegularUserOverview");
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

        public ActionResult AccountDetails(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        [Authorize]

        // GET: RegularUserAddingBankAccounts/Edit/5
        public ActionResult ChangeBankAccountData(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        // POST: RegularUserAddingBankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeBankAccountData(BankAccount account)
        {
            try
            {
                var item = StaticResources.CurrentUser.currentuser;
                var item2 = item.Accounts.Where(a => a.Id == account.Id).FirstOrDefault();
                item2 = account;
                _dataService.UpdateUser(item);

                return RedirectToAction("SubAccountsOverview", "RegularUserOverview");
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

        // GET: RegularUserAddingBankAccounts/Delete/5
        [Authorize]
        public ActionResult RemoveBankAccount(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        // POST: RegularUserAddingBankAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBankAccount(BankAccount account)
        {
            try
            {
                var item = _dataService.ReturnAllUsers().Where(a => a.Accounts.Contains(account)).FirstOrDefault();
                if (item.Accounts.Count <=1)
                {
                    GeneralMethods.ShowMinimumOneBankAccountNeededMessage();
                    return View();
                }
                else
                {
                    item.Accounts.Remove(account);
                    _dataService.UpdateUser(item);
                }
                return RedirectToAction("SubAccountsOverview", "RegularUserOverview");
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
