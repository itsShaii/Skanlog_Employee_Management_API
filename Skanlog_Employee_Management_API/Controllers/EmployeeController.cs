using Microsoft.AspNetCore.Mvc;
using Skanlog_Employee_Management_API.DTO;
using Skanlog_Employee_Management_API.Logic;

namespace Skanlog_Employee_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeLogicAndDbCalls _employeeLogic;

        public EmployeeController(EmployeeLogicAndDbCalls employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            return Ok(await _employeeLogic.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(Guid id)
        {
            var employee = await _employeeLogic.GetEmployeeById(id);
            if (employee == null) return NotFound("Employee not found!");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeDTO employeeDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            bool duplicateEmail = await _employeeLogic.CheckDuplicateEmail(null, employeeDto.Email);
            if (duplicateEmail) return BadRequest("Email already exists!");

            var newEmployee = await _employeeLogic.CreateEmployee(employeeDto);
            if (newEmployee == null) return BadRequest("Error creating employee!");

            return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDTO employeeDto)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            bool duplicateEmail = await _employeeLogic.CheckDuplicateEmail(id, employeeDto.Email);
            if (duplicateEmail) return BadRequest("Email already exists!");

            bool updated = await _employeeLogic.UpdateEmployee(id, employeeDto);
            if (!updated) return NotFound("Employee not found!");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            bool deleted = await _employeeLogic.SoftDeleteEmployee(id);
            if (!deleted) return NotFound("Employee not found!");

            return NoContent();
        }
    }
}
