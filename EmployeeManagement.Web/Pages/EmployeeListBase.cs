using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService iEmployeeService { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; } = true;
        protected int SelectedEmployeesCount { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
            Employees = (await iEmployeeService.GetEmployees()).ToList();
            //await Task.Run(LoadEmployees);
            //LoadEmployees();
            //return base.OnInitializedAsync();
            //var dt = new DataTable();
            //var dtWasChanged = dt.Rows.Cast<DataRow>().Any((r) => r.RowState == DataRowState.Added | r.RowState == DataRowState.Modified | r.RowState == DataRowState.Deleted);


        }

        public async Task EmployeeDeleted()
        {
            Employees = (await iEmployeeService.GetEmployees()).ToList(); 
        }

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected) 
                SelectedEmployeesCount++; 
            else 
                SelectedEmployeesCount--; 
        }
        private void LoadEmployees()
        {
            Thread.Sleep(2000);
            Employee e1 = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 1,
                PhotoPath = "images/john.jpg"
            };

            Employee e2 = new Employee
            {
                Id = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoPath = "images/sam.jpg"
            };

            Employee e3 = new Employee
            {
                Id = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                DepartmentId = 1,
                PhotoPath = "images/mary.jpg"
            };

            Employee e4 = new Employee
            {
                Id = 3,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                DepartmentId = 3,
                PhotoPath = "images/sara.jpg"
            };

            Employees = new List<Employee> { e1, e2, e3, e4 };
        }

        /********* Teste ********/

        CarOptions options = CarOptions.AirConditioning | CarOptions.HeatedSeats;
        public bool? SelectAllState
        {
            get
            {
                if (options == CarOptions.None)
                    return false;
                if (options == CarOptions.All)
                    return true;
                return null;
            }
            set
            {
                if (value.HasValue)
                    options = value.Value ? CarOptions.All : CarOptions.None;
            }
        }
        public bool AirConditioning
        {
            get => options.HasFlag(CarOptions.AirConditioning);
            set => SetOption(value, CarOptions.AirConditioning);
        }
        public bool Multimedia
        {
            get => options.HasFlag(CarOptions.Multimedia);
            set => SetOption(value, CarOptions.Multimedia);
        }
        public bool ParkingSensors
        {
            get => options.HasFlag(CarOptions.ParkingSensors);
            set => SetOption(value, CarOptions.ParkingSensors);
        }
        public bool HeatedSeats
        {
            get => options.HasFlag(CarOptions.HeatedSeats);
            set => SetOption(value, CarOptions.HeatedSeats);
        }
        void SetOption(bool value, CarOptions enumValue)
        {
            if (value)
                options |= enumValue;
            else
                options &= ~enumValue;
        }
        public decimal GetTotalPrice()
        {
            decimal price = 0;
            price += Multimedia ? 130 : 0;
            price += AirConditioning ? 800 : 0;
            price += ParkingSensors ? 400 : 0;
            price += HeatedSeats ? 230 : 0;
            return price;
        }
        public string GetCssClass(bool selected) => selected ? string.Empty : " dx-demo-text-strikethrough";
        [Flags]
        public enum CarOptions
        {
            None = 0x0,
            AirConditioning = 0x1,
            Multimedia = 0x2,
            ParkingSensors = 0x4,
            HeatedSeats = 0x8,
            All = AirConditioning | Multimedia | ParkingSensors | HeatedSeats
        }
    }
}
