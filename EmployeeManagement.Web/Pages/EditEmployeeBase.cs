using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string PageHederText { get; set; } = string.Empty;
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        private Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        [Inject]
        public IMapper Mapper { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editEmployee/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            //Id = Id is null || Id.Contains("-") ? "1" : Id;
            var isIdValid = ushort.TryParse(Id, out ushort employeeId);

            //if (!isIdValid)
            //    return;

            if (employeeId != 0)
            {
                PageHederText = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(ushort.Parse(Id));
            }
            else
            {
                PageHederText = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/noavatar.png"
                };
            }

            //Employee = await EmployeeService.GetEmployee(ushort.Parse(Id));
            Departments = (await DepartmentService.GetDepartments()).ToList();
            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async void HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            //Employee.Department = new Department { Name = Departments.FirstOrDefault((d) => d.DepartmentId == Employee.DepartmentId)?.Name };
            //Employee.DepartmentId = 1;
            var response = await (Employee.Id != 0 ? EmployeeService.UpdateEmployee(Employee.Id, Employee) : EmployeeService.CreateEmployee(Employee));

            if (response == HttpStatusCode.OK)
                NavigationManager.NavigateTo("/", true);
        }
        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.Id);
            //await OnEmployeeDeleted.InvokeAsync(Employee.Id);
            NavigationManager.NavigateTo("/", true);
        }
    }
}
