using DeBank.Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeBankWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {        
        }

        private static ApplicationDbContext _dbContext;

        public static ApplicationDbContext GetDbContext()
        {
            if (_dbContext != null)
            {
                _dbContext = new ApplicationDbContext();
            }
            return _dbContext;
        }

        public DbSet<User> Users { get; set; }
    }
}
