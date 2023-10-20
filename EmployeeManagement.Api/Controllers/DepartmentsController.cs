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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(byte id, Department department)
        {
            try
            {
                var employeeToUpdate = await departmentRepository.GetDepartment(id);

                if (employeeToUpdate is null)
                    return NotFound($"Employee with Id = {department.DepartmentId} not found");

                return await departmentRepository.UpdateDepartment(department);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Department>> DeleteDepartment(byte id)
        {
            try
            {
                var departmentToDelete = await departmentRepository.GetDepartment(id);

                if (departmentToDelete == null) 
                    return NotFound($"Employee with Id = {id} not found"); 

                return await departmentRepository.DeleteDepartment(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            try
            {
                if (department is null)
                    return BadRequest();

                var depId = await departmentRepository.GetDepartment(department.DepartmentId); 

                if (depId != null)
                    return BadRequest("Id já existente.");

                var createdDepartment = await departmentRepository.AddDepartment(department);

                return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.DepartmentId }, createdDepartment);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new employee record");
            }
        }

        private ObjectResult GetStatusCode() => StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
    }
}
