namespace DeBank.Library.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DeBank.Library.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<BankDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BankDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            List<BankAccount> accounts1 = new List<BankAccount>();
            accounts1.Add(new BankAccount { Name = "Persoonlijke Kaart", Money = 100 });

            List<BankAccount> accounts2 = new List<BankAccount>();
            accounts2.Add(new BankAccount { Name = "Persoonlijke Kaart", Money = 100 });

            context.Users.AddOrUpdate(new User { Name = "Vanja van Essen", Accounts = accounts1 });
            context.Users.AddOrUpdate(new User { Name = "Luna Herder", Accounts = accounts2 });
            context.SaveChanges();
        }
    }
}
