using Microsoft.EntityFrameworkCore;
using Workshop.Shared.Entities;

namespace Workshop.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasIndex(x => x.FirstName).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(x => x.LastName).IsUnique();
        modelBuilder.Entity<Employee>().Property(x => x.IsActive);
        modelBuilder.Entity<Employee>().Property(x => x.Salary).HasPrecision(18, 2);
    }
}