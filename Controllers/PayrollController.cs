using Microsoft.AspNetCore.Mvc;
using PayrollService.Models;
using PayrollService.Services;

namespace PayrollService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly ILogger<PayrollController> _logger;

        public PayrollController(IPayrollService payrollService, ILogger<PayrollController> logger)
        {
            _payrollService = payrollService;
            _logger = logger;
        }

        [HttpPost("addSalary")]
        public IActionResult AddSalary([FromBody] PayrollRequest request)
        {
            _logger.LogInformation("Calculate payroll for employee {EmployeeId}", request.EmployeeId);

            var result = _payrollService.CalculatePayroll(request);
            return Ok(result);
        }
    }
}
