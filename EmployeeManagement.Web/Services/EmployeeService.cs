using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components; 

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly System.Net.Http.HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> GetEmployee(ushort id) => await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");

        public async Task<IEnumerable<Employee>> GetEmployees() => await httpClient.GetFromJsonAsync<Employee[]>("api/employees");

        //public Task<Employee> UpdateEmployee(Employee employee)
        //{
        //    //throw new NotImplementedException();
        //}

        //public async Task<Employee> UpdateEmployee(Employee employee) => await httpClient.PutAsJsonAsync<HttpResponseMessage<Employee>>("api/employees", employee);
    }
}

