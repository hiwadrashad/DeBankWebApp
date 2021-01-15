using System.Collections.Generic;
using DeBank.Library.Models;
using DeBank.Library.Logic;
using System;
using DeBank.Library.DAL;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeBank.Tests.Data;

namespace DeBank.Tests
{
    [TestClass]
    public class MoneyTests
    {
        private BankLogic _logic = TestsBankLogic.GetBankLogic();

        [TestMethod]
        public async Task AddMoneyTest()
        {
            User user = await _logic.AddUser("Vanja van Essen");
            BankAccount account = await _logic.AddBankAccount(user, "Persoonlijke Kaart", 0);

            await _logic.AddMoney(account, 10, "Initial money");

            NUnit.Framework.Assert.AreEqual(10, account.Money, "Account didn't get the money correctly");
        }

        [TestMethod]
        public async Task SpendMoneyOnceTest()
        {
            User user = await _logic.AddUser("Vanja van Essen");
            BankAccount account = await _logic.AddBankAccount(user, "Persoonlijke Kaart", 500);

            await _logic.SpendMoney(account, 10);

            Assert.AreEqual(490, account.Money, "Account didn't remove the money correctly");
        }

        [TestMethod]
        public async Task SpendMoneyTwiceTest()
        {
            User user = await _logic.AddUser("Vanja van Essen");
            BankAccount account = await _logic.AddBankAccount(user, "Persoonlijke Kaart", 250);

            await _logic.SpendMoney(account, 10);

            await _logic.SpendMoney(account, 10, true);

            foreach(Transaction transaction in account.PreviousTransactions)
            {
                if(transaction.MayExecuteMore)
                {
                    await transaction.Queue();
                    break;
                }
            }

            Assert.AreEqual(220, account.Money, "Account didn't remove the money correctly");
        }

        [TestMethod]
        public async Task TestAddUser()
        {
            User user = await _logic.AddUser("test");

            Assert.AreEqual("test", user.Name, "Didn't add user correctly");
        }

        [TestMethod]
        public async Task TestAddBankAccount()
        {
            User user = await _logic.AddUser("test");
            BankAccount account = await _logic.AddBankAccount(user, "test", 1000000);

            Assert.AreEqual(user, account.Owner, "Didn't add bank account correctly");
            Assert.AreEqual(1000000, account.Money, "Didn't add bank account correctly");
        }



        [TestMethod]
        [TestCategory("HRTesting")]
        public async Task TestReturnTransactionsWithinTimeFrame()
        {
            BankAccount account = new BankAccount()
            {
                Id = Guid.NewGuid().ToString(),
                DateOfCreation = DateTime.Now,
                PreviousTransactions = new List<Transaction>()
                {
                  new Transaction()
                  { 
                   Id = Guid.NewGuid().ToString(),
                   Amount = 100,
                   LastExecuted = DateTime.Now,
                  }
                }
            };
            NUnit.Framework.Assert.DoesNotThrowAsync(async() => await BankLogic.ReturnTransactionsWithinTimeFrame(account, 100, NumberEnums.Positive));

        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public async Task TestReturnAllUsersBeneathOraboveGivenValue()
        {
            NUnit.Framework.Assert.DoesNotThrowAsync(async() => await _logic.ReturnAllusersBeneathOrAboveGivenValue(100, true));
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public async Task TestReturnAllUserSortedOnName()
        {
            NUnit.Framework.Assert.DoesNotThrowAsync(async() => await _logic.ReturnAllUsersSortedOnName());
        }

        [TestMethod]
        [TestCategory("HRTesting")]
        public async Task TestReturnAllUserSortedOnSaldo()
        {
            NUnit.Framework.Assert.DoesNotThrowAsync(async () => await _logic.ReturnAllUsersSortedOnSaldo());
        }


        [TestMethod]
        [TestCategory("HRTesting")]
        public async Task TestReturnAllUserSortedOndateOfCreation()
        {
            NUnit.Framework.Assert.DoesNotThrowAsync(async () => await _logic.ReturnAllUsersSortedOnDateOfCreation());
        }
    }
}
