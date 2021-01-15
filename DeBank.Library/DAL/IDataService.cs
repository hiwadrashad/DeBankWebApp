using DeBank.Library.GeneralMethods;
using DeBank.Library.Logic;
using DeBank.Library.Models;
using System.Collections.Generic;

namespace DeBank.Library.DAL
{
    public interface IDataService
    {
        bool AddUser(User user);
        bool RemoveUser(User user);
        List<User> ReturnAllUsers();
        List<BankAccount> ReturnAllBankAccounts();
        List<Transaction> ReturnAllTransactions();
        bool UpdateUser(User user);
        Filter<Transaction> ReturnLatestTransactions(int seconds, BankAccount account = null);
        Filter<Transaction> ReturnLatestTransactions(NumberEnums number, int seconds = -1, BankAccount account = null);
        Filter<BankAccount> ReturnAllBankAccountsUnder(decimal thresholdMoney);
        User ReturnUser(string id);
        BankAccount ReturnBankAccount(string id);
        Transaction ReturnTransaction(string id);
    }
}
