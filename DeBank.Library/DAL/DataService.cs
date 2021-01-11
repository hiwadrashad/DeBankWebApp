using DeBank.Library.Interfaces;
using DeBank.Library.Models;
using System.Collections.Generic;
using System.Linq;
using DeBank.Library.Logic;
using DeBank.Library.GeneralMethods;
using System;

namespace DeBank.Library.DAL
{
    public class DataService :IDataService
    {
        private BankDbContext _dbContext = BankDbContext.GetDbContext();

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

        public bool AddBankaccounts(BankAccount bank)
        {
            _dbContext.BankAccounts.Add(bank);
            return true;
        }

        public bool AddTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            return true;
        }

        public bool RemoveUser(User user)
        {
            _dbContext.Users.Remove(user);
            return true;
        }

        public bool RemoveBankaccounts(BankAccount bank)
        {
            _dbContext.BankAccounts.Remove(bank);
            return true;
        }

        public bool RemoveTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Remove(transaction);
            return true;
        }

        public List<User> ReturnAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public List<BankAccount> ReturnAllBankAccounts()
        {
            return _dbContext.BankAccounts.ToList();
        }
        public List<Transaction> ReturnAllTransactions()
        {
            return _dbContext.Transactions.ToList();
        }

        public bool UpdateUser(User user)
        {
            var item = _dbContext.Users.Where(a => a.Id == user.Id).FirstOrDefault();
            item = user;
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateBank(BankAccount bank)
        {
            var item = _dbContext.BankAccounts.Where(a => a.Id == bank.Id).FirstOrDefault();
            item = bank;
            _dbContext.SaveChanges();
            return true;
        }
        public bool UpdateTransactions(Transaction transaction)
        {
            var item = _dbContext.Transactions.Where(a => a.Id == transaction.Id).FirstOrDefault();
            item = transaction;
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
