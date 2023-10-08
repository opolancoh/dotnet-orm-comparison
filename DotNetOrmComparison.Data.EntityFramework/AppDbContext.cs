using DotNetOrmComparison.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetOrmComparison.Data.EntityFramework;

public class AppDbContext: DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    } */
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-One
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Address)
            .WithOne(d => d.Employee)
            .HasForeignKey<Address>(d => d.EmployeeId);

        // One-to-Many
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);

        // Many-to-Many
        modelBuilder.Entity<EmployeeProject>()
            .ToTable("EmployeeProjects"); 
        
        modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Project)
            .WithMany(p => p.EmployeeProjects)
            .HasForeignKey(ep => ep.ProjectId); 
    }
}