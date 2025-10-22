using Microsoft.AspNetCore.Mvc;
using PayrollService.Services;

namespace PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IPayrollService payrollService, ILogger<EmployeeController> logger)
        {
            _payrollService = payrollService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _payrollService.GetAllEmployees();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var emp = _payrollService.GetEmployee(id);
            if (emp == null) return NotFound(new { message = "Employee not found" });
            return Ok(emp);
        }
    }
}
