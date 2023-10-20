using EmployeeManagement.Models;
using EmployeeManagement.Models.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models
{
    public class EditEmployeeModel
    {
        public ushort Id { get; set; }
        [Required(ErrorMessage = "First name must be provider"), StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress, EmailDomainValidator(AllowedDomain = "5linhas.ao", ErrorMessage = "Only 5linhas.ao is allowed")]
        public string Email { get; set; }
        [Compare("Email", ErrorMessage = "Email and Confirm Email must match")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public byte DepartmentId { get; set; }
        //[ValidateComplexType]
        public Department Department { get; set; } = new Department();
        public string PhotoPath { get; set; }
    }
}
