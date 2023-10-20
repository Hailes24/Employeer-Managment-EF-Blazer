using EmployeeManagement.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public ushort Id { get; set; }
        [Required(ErrorMessage = "First name must be provider"), StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress, EmailDomainValidator(AllowedDomain = "5linhas.ao", ErrorMessage = "Only 5linhas.ao is allowed")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public byte DepartmentId { get; set; } 
        //public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
