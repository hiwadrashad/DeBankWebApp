using DeBank.Library.Models;
using DeBank.Library.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace DeBank.Library.Logic
{
    public class BankLogic
    {

        public static async Task<bool> AddMoney(BankAccount account, decimal money, string reason = "")
        {

            Interfaces.IDataService _dataService = DataService.GetDataService();
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = money, Reason = reason, dummytransaction = false, Id = Guid.NewGuid().ToString(), LastExecuted = DateTime.Now };
            transaction.TransactionLog += account.Log;
            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            //<summary>
            //added code start
            //<summary>
            _dataService.AddTransaction(transaction);
            _dataService.UpdateBank(account);
            var UserSuperAccount = _dataService.ReturnAllUsers().Where(a => a.Accounts.Where(a => a.Id == account.Id).Any()).FirstOrDefault();
            _dataService.UpdateUser(UserSuperAccount);
            //<summary>
            //added code end
            //<summary>

            return result;
        }

        //<summary>
        //added code start
        //<summary>
        public static async Task AddUser(string Name, bool dummyornot)
        {
            var lockingobject = new object();
            Monitor.Enter(lockingobject);
            try
            {
                if (dummyornot == false)
                {
                    await Task.Run(() =>
                    {
                        Interfaces.IDataService _dataService = DataService.GetDataService();
                        User user = new User()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = Name,
                            Accounts = new List<BankAccount>()
                            { },
                            dummyaccount = false,
                            dateofcreation = DateTime.Now
                        };
                        _dataService.AddUser(user);
                    });
                }
                else
                {
                    await Task.Run(() =>
                    {
                        Interfaces.IDataService _dataService = DataService.GetDataService();
                        User user = new User()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = Name,
                            Accounts = new List<BankAccount>()
                            { },
                            dummyaccount = true,
                            dateofcreation = DateTime.Now
                        };
                        _dataService.AddUser(user);
                    });
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
            }
            finally
            {
                Monitor.Exit(lockingobject);
            }
        }

        public static async Task AddBankAccount(User owner,string name, decimal money, bool dummyornot)
        {
            var lockingobject = new object();
            Monitor.Enter(lockingobject);
            try
            {
                if (dummyornot == false)
                {
                    await Task.Run(() =>
                    {
                        Interfaces.IDataService _dataService = DataService.GetDataService();
                        BankAccount user = new BankAccount()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Money = money,
                            Name = name,
                            Owner = owner,
                            dummyaccount = false,
                            dateofcreation = DateTime.Now
                        };
                        _dataService.AddBankaccounts(user);
                    });
                }
                else
                {
                    await Task.Run(() =>
                    {
                        Interfaces.IDataService _dataService = DataService.GetDataService();
                        BankAccount user = new BankAccount()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Money = money,
                            Name = name,
                            Owner = owner,
                            dummyaccount = false,
                            dateofcreation = DateTime.Now
                        };
                        _dataService.AddBankaccounts(user);
                    });
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
            }
            finally
            {
                Monitor.Exit(lockingobject);
            }
        }

        public static async Task AutomatedRecurringPayments(decimal price, int amountoftimespayment) //required project assignment
        {
            
            var lockingobject = new object();
            Monitor.Enter(lockingobject);
            using (var transactionrollback = new TransactionScope())
            {
                BankAccount DummyFromAccount = new BankAccount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Money = 1000000,
                    Name = "test",
                    dummyaccount = true,
                    dateofcreation = DateTime.Now
                };

                BankAccount DummyToAccount = new BankAccount()
                {
                     Id = Guid.NewGuid().ToString(),
                     dummyaccount = true,
                     Money = 1000000,
                     Name = "test",
                     dateofcreation = DateTime.Now
                };
                try
                {
                    for (int a = 0; a < amountoftimespayment; a++)
                    {
                        Thread.Sleep(25000);
                        await AddMoney(DummyToAccount, price);
                        await SpendMoney(DummyFromAccount, price);
                    }
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                {
                    GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                }
                finally
                {
                    Monitor.Exit(lockingobject);

                }
            }
        }
#nullable enable
        public static async Task<List<Logic.Transaction>>? ReturnTransactionsWithinTimeFrame(BankAccount user, int timeinseconds, Enums.PositiveNegativeOrAllTransactions positivenegativeornotransactioncheck)
        {
            try
            {
                return await Task.Run(() =>
            {
                var date = DateTime.Now.AddSeconds(-timeinseconds);
                var item = user.PreviousTransactions.Where(a => a.LastExecuted > date).ToList();
                if (positivenegativeornotransactioncheck == Enums.PositiveNegativeOrAllTransactions.none)
                {
                    return item;
                }
                if (positivenegativeornotransactioncheck == Enums.PositiveNegativeOrAllTransactions.positive)
                {
                    return item.Where(a => a.Amount > 0).ToList();
                }
                else
                {
                    return item.Where(a => a.Amount < 0).ToList();

                }
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }


        public static async Task<List<BankAccount>>? ReturnAllusersBeneathOrAboveGivenValue(decimal saldolimit, bool AboveValue)
        {
            try
            {
                Interfaces.IDataService _dataService = DataService.GetDataService();

                if (AboveValue == true)
                {
                    return await Task.Run(() =>
                    {
                        return _dataService.ReturnAllBankAccounts().Where(a => a.Money > saldolimit).ToList();
                    }
                    );
                }
                else
                {
                    return await Task.Run(() =>
                    {
                        return _dataService.ReturnAllBankAccounts().Where(a => a.Money < saldolimit).ToList();
                    }
                   );
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }


        public static async Task<List<BankAccount>>? ReturnAllUsersSortedOnName()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderBy(a => a.Name).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public static async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnName()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderByDescending(a => a.Name).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public static async Task<List<BankAccount>>? ReturnAllUsersSortedOnSaldo()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderBy(a => a.Money).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }


        public static async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnSaldo()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderByDescending(a => a.Money).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public static async Task<List<BankAccount>>? ReturnAllUsersSortedOnDateOfCreation()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderBy(a => a.dateofcreation).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }


        public static async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnDateOfCreation()
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();
            try
            {
                return await Task.Run(() =>
            {
                return _dataService.ReturnAllBankAccounts().OrderByDescending(a => a.dateofcreation).ToList();
            }
            );
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (NullReferenceException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (ArgumentNullException ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
#nullable disable
        //<summary>
        //added code end
        //<summary>
        public static async Task<bool> SpendMoney(BankAccount account, decimal money, bool subscription = false, string reason = "")
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();

            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = -money, Reason = reason, MayExecuteMore = subscription , Id = Guid.NewGuid().ToString(), dummytransaction = false, LastExecuted = DateTime.Now};
            transaction.TransactionLog += account.Log;

            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            //<summary>
            //added code start
            //<summary>
            _dataService.AddTransaction(transaction);
            _dataService.UpdateBank(account);
            var UserSuperAccount = _dataService.ReturnAllUsers().Where(a => a.Accounts.Where(a => a.Id == account.Id).Any()).FirstOrDefault();
            _dataService.UpdateUser(UserSuperAccount);
            //<summary>
            //added code end
            //<summary>

            return result;
        }

        public static async Task<bool> TransferMoney(BankAccount account1, BankAccount account2, decimal money, string reason = "")
        {
            Interfaces.IDataService _dataService = DataService.GetDataService();

            if (money < 0)
            {
                return false;
            }

            bool result = await SpendMoney(account1, money, false, "Geld overmaken naar " + account2.Owner.Name + (reason != "" ? ": " + reason : ""));
            if (result)
            {
                await AddMoney(account2, money, "Geld overmaken van " + account1.Owner.Name + (reason != "" ? ": " + reason : ""));
            }

            return result;
        }
    }
}
