using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeBank.Library.DAL;
using System.Collections;
using System.Collections.Generic;
using DeBank.Library.Models;
using DeBank.Library.GeneralMethods;
using System.Linq;

namespace DeBank.Tests
{
    [TestClass]
    public class FilterTests
    {
        public IEnumerable<User> GetDummyUsers()
        {
            List<User> users = new List<User>();

            List<BankAccount> accounts1 = new List<BankAccount>();
            accounts1.Add(new BankAccount { Name = "Persoonlijke Kaart" });
            users.Add(new User { Name = "Vanja van Essen", Accounts = accounts1 });

            List<BankAccount> accounts2 = new List<BankAccount>();
            accounts2.Add(new BankAccount { Name = "Coole Kaart" });
            users.Add(new User { Name = "John Doe", Accounts = accounts2 });

            List<BankAccount> accounts3 = new List<BankAccount>();
            accounts3.Add(new BankAccount { Name = "Epik Kaart" });
            users.Add(new User { Name = "Joe Doe", Accounts = accounts3 });

            return users;
        }

        [TestMethod]
        public void CustomFilter()
        {
            Filter<User> filter = new Filter<User>(GetDummyUsers()).AddFilter(u => u.Name.EndsWith("Doe"));

            IEnumerable<User> users = filter.Execute();

            Assert.AreEqual(GetDummyUsers().ElementAt(1).Name, users.ToList()[0].Name);
        }
    }
}
