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
        // GET: RegularUserMoneyMutation
        public ActionResult TransferMoney()
        {
            TransactionViewModel viewmodel = new TransactionViewModel();
            //viewmodel.transaction = new ()
            //{

            //};
            return View();
        }

        // GET: RegularUserMoneyMutation/Details/5
        //[HttpPost]
        //public ActionResult TransferMoney()
        //{
        //    return View();
        //}


        // GET: RegularUserMoneyMutation/Edit/5
        public ActionResult Pay(int id)
        {
            return View();
        }

        // POST: RegularUserMoneyMutation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pay(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
