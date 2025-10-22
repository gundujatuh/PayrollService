using Microsoft.EntityFrameworkCore;
using PayrollService.Models;

namespace PayrollService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "ini nama1", BaseSalary = 5000m, HoursWorked = 160 },
                new Employee { Id = 2, Name = "ini nama2", BaseSalary = 6000m, HoursWorked = 170 },
                new Employee { Id = 3, Name = "ini nama3", BaseSalary = 4500m, HoursWorked = 150 }
            );
        }
    }
}
