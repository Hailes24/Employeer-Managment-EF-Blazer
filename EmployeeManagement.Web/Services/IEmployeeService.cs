using EmployeeManagement.Models;
using System.Net;

namespace EmployeeManagement.Web.Services
{
    public interface IEmployeeService
    {
        Task<HttpStatusCode> CreateEmployee(Employee employee);
        Task<object> DeleteEmployee(ushort id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(ushort id);
        Task<HttpStatusCode> UpdateEmployee(ushort id, Employee employee);
    }
}
