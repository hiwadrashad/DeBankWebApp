using DeBank.Library.Models;
using System.Collections.Generic;
using System.Linq;
using DeBank.Library.Logic;
using DeBank.Library.GeneralMethods;
using System;
using DeBank.Library.DAL;

namespace DeBankWebApp.Data
{
    public class DataService 
    {
        private ApplicationDbContext _dbContext = ApplicationDbContext.GetDbContext();

        private static DataService _dataService;

        private DataService()
        { 
        
        }

        public static DataService GetDataService()
        {
            if (_dataService == null)
            {
                _dataService = new DataService();
            }
            return _dataService;
        }

        public bool AddUser(User user)
        {
            _dbContext.Users.Add(user);
            return true;
        }

        public bool RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
            return true;
        }

        public List<User> ReturnAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public List<BankAccount> ReturnAllBankAccounts()
        {
            List<BankAccount> accounts = new List<BankAccount>();

            foreach(User user in ReturnAllUsers())
            {
                accounts.AddRange(user.Accounts);
            }

            return accounts;
        }

        public List<Transaction> ReturnAllTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach (BankAccount user in ReturnAllBankAccounts())
            {
                transactions.AddRange(user.PreviousTransactions);
            }

            return transactions;
        }

        public User ReturnUser(string id)
        {
            return _dbContext.Users.Where(a => a.Id == id).FirstOrDefault();
        }

        public BankAccount ReturnBankAccount(string id)
        {
            return ReturnAllBankAccounts().Where(a => a.Id == id).FirstOrDefault();
        }

        public Transaction ReturnTransaction(string id)
        {
            return ReturnAllTransactions().Where(a => a.Id == id).FirstOrDefault();
        }

        public bool UpdateUser(User user)
        {
            var item = _dbContext.Users.Where(a => a.Id == user.Id).FirstOrDefault();
            item = user;
            _dbContext.SaveChanges();
            return true;
        }

        public Filter<Transaction> ReturnLatestTransactions(int seconds, BankAccount account = null)
        {
            Filter<Transaction> filter = new Filter<Transaction>(ReturnAllTransactions()).AddFilter(t => t.LastExecuted >= DateTime.Now.AddSeconds(-seconds));

            if (account != null)
            {
                filter.AddFilter(t => t.Account == account);
            }

            return filter;
        }

        public Filter<Transaction> ReturnLatestTransactions(NumberEnums number, int seconds = -1, BankAccount account = null)
        {
            Filter<Transaction> filter = new Filter<Transaction>(ReturnAllTransactions()).AddFilter(t => {
                if (number == NumberEnums.Positive)
                {
                    return t.Amount > 0;
                }
                else if(number == NumberEnums.Negative)
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
