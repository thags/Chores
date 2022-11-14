using System;
using Chores.Models;
using Microsoft.EntityFrameworkCore;

namespace Chores.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chore> Chores { get; set; }
    }
}

