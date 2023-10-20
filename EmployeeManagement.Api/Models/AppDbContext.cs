using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public void EnableIdentityInsert()
        {
            Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees ON;");
        }

        public void DisableIdentityInsert()
        {
            Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees OFF;");
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Seed Departments Table
            modelBuilder.Entity<Department>().HasData( 
                new[] { 
                    new Department { DepartmentId = 1, Name = "IT" }, 
                    new Department { DepartmentId = 2, Name = "HR" }, 
                    new Department { DepartmentId = 3, Name = "Payroll" }, 
                    new Department { DepartmentId = 4, Name = "Admin" } 
                }
            );

            modelBuilder.Entity<Employee>().HasData(
                 new[] {
                    new Employee
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Hastings",
                        Email = "David@pragimtech.com",
                        DateOfBrith = new DateTime(1980, 10, 5),
                        Gender = Gender.Male,
                        DepartmentId = 1,
                        PhotoPath = "images/john.jpg"
                    },
                    new Employee
                    {
                        Id = 2,
                        FirstName = "Sam",
                        LastName = "Galloway",
                        Email = "Sam@pragimtech.com",
                        DateOfBrith = new DateTime(1981, 12, 22),
                        Gender = Gender.Male,
                        DepartmentId = 2,
                        PhotoPath = "images/sam.jpg",
                    },
                    new Employee
                    {
                        Id = 3,
                        FirstName = "Mary",
                        LastName = "Smith",
                        Email = "mary@pragimtech.com",
                        DateOfBrith = new DateTime(1979, 11, 11),
                        Gender = Gender.Female,
                        DepartmentId = 1,
                        PhotoPath = "images/mary.jpg"
                    },
                    new Employee
                    {
                        Id = 4,
                        FirstName = "Sara",
                        LastName = "Longway",
                        Email = "sara@pragimtech.com",
                        DateOfBrith = new DateTime(1982, 9, 23),
                        Gender = Gender.Female,
                        DepartmentId = 3,
                        PhotoPath = "images/sara.jpg"
                    },
                }
            );
        }
    }
}
