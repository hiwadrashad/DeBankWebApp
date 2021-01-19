using DeBank.Library.DAL;
using DeBank.Library.GeneralMethods;
using DeBank.Library.Models;
using DeBankWebApp.Data;
using DeBankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public RegularUserAddingAndChangeBankAccounts(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        // GET: RegularUserAddingBankAccounts/Create
        [Authorize]
        public IActionResult GenerateBankAccount()
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
        [Authorize]

        public IActionResult AssignRole()
        {
            AdminInvocation item = new AdminInvocation();
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(AdminInvocation inputcode)
        {
            if (inputcode.Input == _dataService.ReturnAdminCode())
            {
                IdentityUser item = await _userManager.FindByNameAsync(User.Identity.Name);
                await _userManager.AddToRoleAsync(item, "Admin");
                return RedirectToAction("SuccesfullAdminAsertion", "RegularUserAddingAndChangeBankAccounts");
            }
            else
            {
                return RedirectToAction("FailedAdminAssertion", "RegularUserAddingAndChangeBankAccounts");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowCurrentUpgradeCode()
        {
            ViewBag.Code = _dataService.ReturnAdminCode();
            return View();
        }
        [Authorize]
        public IActionResult FailedAdminAssertion()
        {
            return View();
        }
        [Authorize]
        public IActionResult SuccesfullAdminAsertion()
        {
            return View();
        }

        // POST: RegularUserAddingBankAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateBankAccount(BankAccount account)
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

        public IActionResult AccountDetails(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        [Authorize]

        // GET: RegularUserAddingBankAccounts/Edit/5
        public IActionResult ChangeBankAccountData(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        // POST: RegularUserAddingBankAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeBankAccountData(BankAccount account)
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
        public IActionResult RemoveBankAccount(string id)
        {
            return View(_dataService.ReturnBankAccount(id));
        }

        // POST: RegularUserAddingBankAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBankAccount(BankAccount account)
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
