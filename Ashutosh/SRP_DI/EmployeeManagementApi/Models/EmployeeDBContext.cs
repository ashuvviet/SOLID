using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class EmployeeDBContext : DbContext
	{
        public EmployeeDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                       .HasOne(x => x.Insurance)
                       .WithOne()
                       .HasForeignKey<Insurance>(b => b.Id);

            modelBuilder.Entity<Employee>()
                   .HasOne(x => x.Salary)
                   .WithOne()
                   .HasForeignKey<Payment>(b => b.Id);
    
        }
    }
}
