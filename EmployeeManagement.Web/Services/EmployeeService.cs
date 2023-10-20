using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpStatusCode> CreateEmployee(Employee employee)
        {
            return (await httpClient.PostAsJsonAsync($"api/employees", employee)).StatusCode;
        }

        public async Task<object> DeleteEmployee(ushort id)
        {
            return await httpClient.DeleteFromJsonAsync($"api/employees/{id}", typeof(object));
        }

        public async Task<Employee> GetEmployee(ushort id) => await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");

        public async Task<IEnumerable<Employee>> GetEmployees() => await httpClient.GetFromJsonAsync<Employee[]>("api/employees");

        public async Task<HttpStatusCode> UpdateEmployee(ushort id, Employee employee) => (await httpClient.PutAsJsonAsync($"api/employees/{employee.Id}", employee)).StatusCode;
        
    }
}

