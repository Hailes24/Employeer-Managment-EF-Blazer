using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Department
    {
        public byte DepartmentId { get; set; }
        [Required(ErrorMessage = "Department name must be provider")]
        public string Name { get; set; }
    }
}
