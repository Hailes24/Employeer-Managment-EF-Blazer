using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await departmentRepository.GetDepartments());
            }
            catch (Exception)
            {
                return GetStatusCode();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartment(byte id)
        {
            try
            {
                var departments = await departmentRepository.GetDepartment(id);

                if (departments == null)
                    return NotFound();
                return departments;
            }
            catch (Exception)
            {
                return GetStatusCode();
            }
        }

        private ObjectResult GetStatusCode() => StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
    }
}
