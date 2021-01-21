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
        private readonly IDataService _service;

        //public BankLogic(IDataService)
        //{
        //    _service = service;
        //}

        public async Task<bool> AddMoney(BankAccount account, decimal money, string reason = "")
        {
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Account = account, Amount = money, Reason = reason, Id = Guid.NewGuid().ToString(), LastExecuted = DateTime.Now };
            transaction.TransactionLog += account.Log;

            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            return result;
        }

        public async Task<User> AddUser(string Name)
        {
            return await Task.Run(() =>
            {
                User user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Accounts = new List<BankAccount>()
                    { },
                    DateOfCreation = DateTime.Now
                };
                _service.AddUser(user);

                return user;
            });

        }

        public async Task<BankAccount> AddBankAccount(User owner, string name, decimal money)
        {
            return await Task.Run(() =>
            {
                BankAccount account = new BankAccount()
                {
                    Id = Guid.NewGuid().ToString(),
                    Money = money,
                    Name = name,
                    Owner = owner,
                    DateOfCreation = DateTime.Now
                };
                owner.Accounts.Add(account);
                _service.UpdateUser(owner);

                return account;
            });

        }

        public async Task AutomatedRecurringPayments(decimal price, int amountoftimespayment) //required project assignment
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
                    DateOfCreation = DateTime.Now
                };

                BankAccount DummyToAccount = new BankAccount()
                {
                     Id = Guid.NewGuid().ToString(),
                     Money = 1000000,
                     Name = "test",
                    DateOfCreation = DateTime.Now
                };
                try
                {
                    for (int a = 0; a < amountoftimespayment; a++)
                    {
                        Thread.Sleep(25000);
                        BankLogic item = new BankLogic();
                        await item.AddMoney(DummyToAccount, price);
                        await item.SpendMoney(DummyFromAccount, price);
                    }
                }
                catch (NullReferenceException ex)
                {
                    GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                }
                catch (ArgumentNullException ex)
                {
                    GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                }
                catch (Exception ex)
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
        public async Task<List<Transaction>>? ReturnTransactionsWithinTimeFrame(BankAccount user, int timeinseconds, NumberEnums numberEnum)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var date = DateTime.Now.AddSeconds(-timeinseconds);
                    var item = user.PreviousTransactions.Where(a => a.LastExecuted > date).ToList();
                    if (numberEnum == NumberEnums.Positive)
                    {
                        return item.Where(a => a.Amount > 0).ToList();
                    }
                    else
                    {
                        return item.Where(a => a.Amount < 0).ToList();

                    }
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }


        public async Task<List<BankAccount>>? ReturnAllusersBeneathOrAboveGivenValue(decimal saldolimit, bool AboveValue)
        {
            try
            {
                if (AboveValue == true)
                {
                    return await Task.Run(() =>
                    {
                        return _service.ReturnAllBankAccounts().Where(a => a.Money > saldolimit).ToList();
                    });
                }
                else
                {
                    return await Task.Run(() =>
                    {
                        return _service.ReturnAllBankAccounts().Where(a => a.Money < saldolimit).ToList();
                    });
                }
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }


        public async Task<List<BankAccount>>? ReturnAllUsersSortedOnName()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderBy(a => a.Name).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }

        public async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnName()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderByDescending(a => a.Name).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }

        public async Task<List<BankAccount>>? ReturnAllUsersSortedOnSaldo()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderBy(a => a.Money).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }


        public async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnSaldo()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderByDescending(a => a.Money).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }

        public async Task<List<BankAccount>>? ReturnAllUsersSortedOnDateOfCreation()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderBy(a => a.DateOfCreation).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }


        public async Task<List<BankAccount>>? ReturnAllUsersReverseSortedOnDateOfCreation()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _service.ReturnAllBankAccounts().OrderByDescending(a => a.DateOfCreation).ToList();
                });
            }
            catch (NullReferenceException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (ArgumentNullException ex)
            {
                GeneralMethods.GeneralMethods.ShowIncorrectValueErrorMessage();
                return null;
            }
            catch (Exception ex)
            {
                GeneralMethods.GeneralMethods.ShowGeneralErrorMessage();
                return null;
            }
        }

        public async Task<bool> SpendMoney(BankAccount account, decimal money, bool subscription = false, string reason = "")
        {
            if (money < 0)
            {
                return false;
            }

            Transaction transaction = new Transaction { Id = Guid.NewGuid().ToString(), Account = account, Amount = -money, Reason = reason, MayExecuteMore = subscription, LastExecuted = DateTime.Now};
            transaction.TransactionLog += account.Log;

            account.TransactionQueue.Add(transaction);
            bool result = await transaction.Queue();
            account.TransactionQueue.Remove(transaction);

            account.PreviousTransactions.Add(transaction);

            return result;
        }

        public async Task<bool> TransferMoney(BankAccount account1, BankAccount account2, decimal money, string reason = "")
        {
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
