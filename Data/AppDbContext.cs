using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Expense> Expense => Set<Expense>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
