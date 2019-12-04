using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jupiter.Budgetary.Models
{
    public class BudgetaryContext : DbContext
    {
        public BudgetaryContext(DbContextOptions<BudgetaryContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}