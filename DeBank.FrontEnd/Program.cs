using DeBank.FrontEnd.Data;
using DeBank.Library;
using DeBank.Library.Logic;
using DeBank.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DeBank.FrontEnd
{
    internal struct TransferMoneyStruct
    {
        public BankAccount Account1 { get; set; }
        public BankAccount Account2 { get; set; }

        public List<User> Users { get; set; }
    }

    class Program
    {
        private static BankLogic _logic = new BankLogic(DataService.GetDataService());

        static void Main(string[] args)
        {
            using (var db = new BankDbContext())
            {
                Console.WriteLine("Users:");
                foreach (User user in db.Users)
                {
                    Console.WriteLine("  " + user.Name + ":");
                    foreach (BankAccount account in user.Accounts)
                    {
                        account.TransactionLog += Account_TransactionLog;

                        Console.WriteLine("  - " + account.Name + ":");
                        Console.WriteLine("     Money: " + account.Money);
                    }
                }
                
                User user1 = db.Users.Where(u => u.Name == "Vanja van Essen").FirstOrDefault();
                User user2 = db.Users.Where(u => u.Name == "Luna Herder").FirstOrDefault();

                BankAccount account1 = user1.Accounts.FirstOrDefault();
                BankAccount account2 = user2.Accounts.FirstOrDefault();

                Thread transferMoneyThread = new Thread(TransferMoney);
                transferMoneyThread.Start(new TransferMoneyStruct { Account1 = account1, Account2 = account2, Users = db.Users.ToList() });
            }
        }

        private static async void TransferMoney(object obj)
        {
            if(!(obj is TransferMoneyStruct))
            {
                return;
            }

            TransferMoneyStruct strct = (TransferMoneyStruct) obj;

            await _logic.TransferMoney(strct.Account1, strct.Account2, 10, "Testen");

            Console.WriteLine("Users (AFTER):");
            foreach (User user in strct.Users)
            {
                Console.WriteLine("  " + user.Name + ":");
                foreach (BankAccount account in user.Accounts)
                {
                    Console.WriteLine("  - " + account.Name + ":");
                    Console.WriteLine("     Money: " + account.Money);
                }
            }
        }

        private static void Account_TransactionLog(object sender, string e)
        {
            Console.WriteLine("LOG (" + sender + "): " + e);
        }
    }
}
