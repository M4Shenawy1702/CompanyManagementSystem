using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.UserId);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.BaseSalary)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.Bonus)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.Deduction)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Payroll>()
                .Property(p => p.TotalSalary)
                .HasColumnType("decimal(18, 4)");

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payrolls)
                .HasForeignKey(p => p.UserId);

        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
