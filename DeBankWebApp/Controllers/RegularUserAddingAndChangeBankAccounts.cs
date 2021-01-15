using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Models;
using DeBankWebApp.Data;
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
        IDataService _dataService = DeBank.Library.DAL.MockingData.GetMockDataService();
        // GET: RegularUserAddingBankAccounts/Create
        public ActionResult GenerateBankAccount()
        {
            BankAccount account = new BankAccount()
            {
                Id = Guid.NewGuid().ToString(),
                DateOfCreation = DateTime.Now,
                IBAN = IBAN.IBAN.GenerateIBANNumber(),
                Owner = StaticResources.CurrentUser.currentuser,
            };
            return View(account);
        }

        // POST: RegularUserAddingBankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenerateBankAccount(BankAccount account)
        {
            try
            {
                IbanNet.Builders.IbanBuilder builder = new IbanNet.Builders.IbanBuilder();
                var IBANNumber = builder.Build();
                account.IBAN = IBANNumber;
                var item = StaticResources.CurrentUser.currentuser;
                item.Accounts.Add(account);
                _dataService.UpdateUser(item);
                return RedirectToAction("SubAccountsOverview", "RegularUserOverview");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AccountDetails(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

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
            catch
            {
                return View();
            }
        }

        // GET: RegularUserAddingBankAccounts/Delete/5
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
            catch
            {
                return View();
            }
        }
    }
}
