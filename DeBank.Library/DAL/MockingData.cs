using DeBank.Library.GeneralMethods;
using DeBank.Library.Logic;
using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeBank.Library.DAL
{
    public class MockingData :IDataService
    {
        private List<User> _users;
        private List<BankAccount> _bankaccounts;
        private List<Transaction> _transactions;
        private string _currentAdminUpgradeCode;

        private static MockingData _MockingdataService;

        public string IBAN { get; private set; }


        private MockingData()
        {

        }

        public static MockingData GetMockDataService()
        {
            if (_MockingdataService == null)
            {
                _MockingdataService = new MockingData();
                _MockingdataService.InitData();
            }
            return _MockingdataService;
        }

        public void InitData()
        {
        _users = new List<User>()
        {
         new User()
         { 
          Id = Guid.NewGuid().ToString(),
         DateOfCreation = DateTime.Now,
         Name = "test",
         Info = new Information()
         {
          Addition = "Test",
          City = "Test",
          Emailadress = "Test@hotmail.com",
          Firstname = "Test",
          Lastname = "Test",
          Postalcode = "Test",
          Streetname = "Test",
          Streetnumber = "Test",
          Telephonenumber = "Test",
         },
         }
        };
            _bankaccounts = new List<BankAccount>()
        {
         new BankAccount()
         {
          Id = Guid.NewGuid().ToString(),
          DateOfCreation = DateTime.Now,
          info = new Information()
         {
          Addition = "Test",
          City = "Test",
          Emailadress = "Test@hotmail.com",
          Firstname = "Test",
          Lastname = "Test",
          Postalcode = "Test",
          Streetname = "Test",
          Streetnumber = "Test",
          Telephonenumber = "Test",
         },
         Money = 1000000,
         Name = "Test",
         Owner = _users.FirstOrDefault()
         },
           new BankAccount()
         {
          Id = Guid.NewGuid().ToString(),
          DateOfCreation = DateTime.Now,
          info = new Information()
         {
          Addition = "Test",
          City = "Test",
          Emailadress = "Test@hotmail.com",
          Firstname = "Test",
          Lastname = "Test",
          Postalcode = "Test",
          Streetname = "Test",
          Streetnumber = "Test",
          Telephonenumber = "Test",
         },
         Money = 1000000,
         Name = "Test",
         Owner = _users.FirstOrDefault()
         }
        };

            _users.FirstOrDefault().Accounts = _bankaccounts;

            _transactions = new List<Transaction>()
            {
              new Transaction()
              { 
               Account = _bankaccounts.FirstOrDefault(),
               Amount = 100,
               Id = Guid.NewGuid().ToString(),
               InteractedAccount = _bankaccounts.Skip(1).FirstOrDefault(),
               Reason = "Test"
              }
            };
        }
        public void GenerateAdminUpgradeCode()
        {
            _currentAdminUpgradeCode = Guid.NewGuid().ToString();
        }

        public string ReturnAdminCode()
        {
            return _currentAdminUpgradeCode;
        }
        public bool AddUser(User user)
        {
            _users.Add(user);
            return true;
        }

        public bool AddBankaccounts(BankAccount bank)
        {
            _bankaccounts.Add(bank);
            return true;
        }

        public bool AddTransaction(Logic.Transaction transaction)
        {
            _transactions.Add(transaction);
            return true;
        }

        public bool RemoveUser(User user)
        {
            _users.Remove(user);
            return true;
        }

        public bool RemoveBankaccounts(BankAccount bank)
        {
            _bankaccounts.Remove(bank);
            return true;
        }

        public bool RemoveTransaction(Logic.Transaction transaction)
        {
            _transactions.Remove(transaction);
            return true;
        }

        public List<User> ReturnAllUsers()
        {
            return _users.ToList();
        }

        public List<BankAccount> ReturnAllBankAccounts()
        {
            return _bankaccounts.ToList();
        }
        public List<Logic.Transaction> ReturnAllTransactions()
        {
            return _transactions.ToList();
        }

        public User ReturnUser(string id)
        {
            return _users.Where(a => a.Id == id).FirstOrDefault();
        }

        public BankAccount ReturnBankAccount(string id)
        {
            return _bankaccounts.Where(a => a.Id == id).FirstOrDefault();
        }

        public Logic.Transaction ReturnTransaction(string id)
        {
            return _transactions.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool UpdateUser(User user)
        {
            var item = _users.Where(a => a.Id == user.Id).FirstOrDefault();
            item = user;
            return true;
        }

        public bool UpdateBank(BankAccount bank)
        {
            var item = _bankaccounts.Where(a => a.Id == bank.Id).FirstOrDefault();
            item = bank;
            return true;
        }
        public bool UpdateTransactions(Logic.Transaction transaction)
        {
            var item = _transactions.Where(a => a.Id == transaction.Id).FirstOrDefault();
            item = transaction;
            return true;
        }

        public Filter<Logic.Transaction> ReturnLatestTransactions(int seconds, BankAccount account = null)
        {
            Filter<Logic.Transaction> filter = new Filter<Logic.Transaction>(ReturnAllTransactions()).AddFilter(t => t.LastExecuted >= DateTime.Now.AddSeconds(-seconds));

            if (account != null)
            {
                filter.AddFilter(t => t.Account == account);
            }

            return filter;
        }

        public Filter<Logic.Transaction> ReturnLatestTransactions(NumberEnums number, int seconds = -1, BankAccount account = null)
        {
            Filter<Logic.Transaction> filter = new Filter<Logic.Transaction>(ReturnAllTransactions()).AddFilter(t => {
                if (number == NumberEnums.Positive)
                {
                    return t.Amount > 0;
                }
                else if (number == NumberEnums.Negative)
                {
                    return t.Amount < 0;
                }
                else
                {
                    return t.Amount > 0;
                }
            });

            if (seconds >= 0)
            {
                return filter.AddFilter(t => t.LastExecuted >= DateTime.Now.AddSeconds(-seconds));
            }

            if (account != null)
            {
                filter.AddFilter(t => t.Account == account);
            }

            return filter;
        }

        public Filter<BankAccount> ReturnAllBankAccountsUnder(decimal thresholdMoney)
        {
            return new Filter<BankAccount>(ReturnAllBankAccounts()).AddFilter(t => t.Money < thresholdMoney);
        }
    }
}
