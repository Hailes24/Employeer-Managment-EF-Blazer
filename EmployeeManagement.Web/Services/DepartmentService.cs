using EmployeeManagement.Models;

namespace EmployeeManagement.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Department> GetDepartment(byte id) => await httpClient.GetFromJsonAsync<Department>($"api/departments/{id}");

        public async Task<IEnumerable<Department>> GetDepartments() => await httpClient.GetFromJsonAsync<Department[]>("api/departments");
    }
}
