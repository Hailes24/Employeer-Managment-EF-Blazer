using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeAdded = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return employeeAdded.Entity;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var employeeToDelect = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (isValidEmployee(employeeToDelect))
            {
                appDbContext.Employees.Remove(employeeToDelect);
                await appDbContext.SaveChangesAsync();
                return employeeToDelect;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int employeeId) => await appDbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == employeeId);

        public async Task<Employee> GetEmployeeByEmail(string email) => await appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);

        public async Task<IEnumerable<Employee>> GetEmployees() => await appDbContext.Employees.ToListAsync();

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> employees = appDbContext.Employees;

            if (!string.IsNullOrEmpty(name))
                employees = employees.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));

            if (gender != null)
                employees = employees.Where(e => e.Gender == gender);

            return await employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var employeeToUpload = await appDbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
             
            if (!isValidEmployee(employeeToUpload))
                return null;

            employeeToUpload.FirstName = employee.FirstName;
            employeeToUpload.LastName = employee.LastName;
            employeeToUpload.Email = employee.Email;
            employeeToUpload.DateOfBrith = employee.DateOfBrith;
            employeeToUpload.Gender = employee.Gender;
            employeeToUpload.DepartmentId = employee.DepartmentId;
            employeeToUpload.PhotoPath = employee.PhotoPath;

            await appDbContext.SaveChangesAsync();
            return employeeToUpload;
        }

        private bool isValidEmployee(Employee employee) => employee != null; 
    }
}
