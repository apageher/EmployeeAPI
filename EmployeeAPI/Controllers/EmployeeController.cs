using EmployeeAPI.Services.EmployeeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>>? GetEmployeeById(int id)
        {
            var result = _employeeService.GetEmployeeById(id);
            if (result == null)
                return NotFound("Employee not found");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee newEmployee)
        {
            var result = _employeeService.AddEmployee(newEmployee);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(int id, Employee updatedEmployee)
        {
            var result = _employeeService.UpdateEmployee(id, updatedEmployee);
            if (result == null)
                return NotFound("Employee not found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            if (result == null)
                return NotFound("Employee not found");
            return Ok(result);
        }
    }
}
