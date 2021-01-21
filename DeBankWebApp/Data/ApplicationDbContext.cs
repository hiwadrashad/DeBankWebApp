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
        private static DbContextOptions<ApplicationDbContext> _options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _options = options;
        }


        private static ApplicationDbContext _dbContext;

        public static ApplicationDbContext GetDbContext()
        {
            if (_dbContext != null)
            {
                _dbContext = new ApplicationDbContext(_options);
            }
            return _dbContext;
        }

        public DbSet<User> Users { get; set; }
    }
}
