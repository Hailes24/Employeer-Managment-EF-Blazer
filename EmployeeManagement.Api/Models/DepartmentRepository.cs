using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Department> AddDepartment(Department department)
        {
            var departmentAdded = await appDbContext.Departments.AddAsync(department);
            await appDbContext.SaveChangesAsync();
            return departmentAdded.Entity;
        }

        public async Task<Department> DeleteDepartment(byte departmentId)
        {
            var departmentToDelect = await appDbContext.Departments.FirstOrDefaultAsync((d) => d.DepartmentId == departmentId);

            if (isValidEmployee(departmentToDelect))
            {
                appDbContext.Departments.Remove(departmentToDelect);
                await appDbContext.SaveChangesAsync();
                return departmentToDelect;
            }
            return null;
        }

        public async Task<Department> GetDepartment(byte id) => await appDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

        public async Task<IEnumerable<Department>> GetDepartments() => await appDbContext.Departments.ToListAsync();

        public async Task<Department> UpdateDepartment(Department department)
        {
            var departmentToUpload = await appDbContext.Departments.FirstOrDefaultAsync((d) => d.DepartmentId == department.DepartmentId);

            if (!isValidEmployee(departmentToUpload))
                return null;

            departmentToUpload.DepartmentId = department.DepartmentId;
            departmentToUpload.Name = department.Name; 

            await appDbContext.SaveChangesAsync();
            return departmentToUpload;
        }
        private bool isValidEmployee(Department employee) => employee != null;
    }
}
