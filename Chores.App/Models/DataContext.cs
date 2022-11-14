using System;
using Microsoft.EntityFrameworkCore;

namespace Chores.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Chore> Chores { get; set; }
    }
}

