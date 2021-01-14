using DeBank.Library.GeneralMethods;
using DeBank.Library.Logic;
using DeBank.Library.Models;
using System.Collections.Generic;

namespace DeBank.Library.Interfaces
{
    public interface IDataService
    {
        bool AddUser(User user);
        bool AddBankaccounts(BankAccount bank);
        bool AddTransaction(Transaction transaction);
        bool RemoveUser(User user);
        bool RemoveBankaccounts(BankAccount bank);
        bool RemoveTransaction(Transaction transaction);
        List<User> ReturnAllUsers();
        List<BankAccount> ReturnAllBankAccounts();
        List<Transaction> ReturnAllTransactions();
        bool UpdateUser(User user);
        bool UpdateBank(BankAccount bank);
        bool UpdateTransactions(Transaction transaction);
        Filter<Transaction> ReturnLatestTransactions(int seconds, BankAccount account = null);
        Filter<Transaction> ReturnLatestTransactions(NumberEnums number, int seconds = -1, BankAccount account = null);
        Filter<BankAccount> ReturnAllBankAccountsUnder(decimal thresholdMoney);
        User ReturnUser(string id);
        BankAccount ReturnBankAccount(string id);
        Logic.Transaction ReturnTransaction(string id);


    }
}
