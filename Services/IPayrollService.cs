using PayrollService.Models;

namespace PayrollService.Services
{
    public interface IPayrollService
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee? GetEmployee(int id);
        PayrollResult CalculatePayroll(PayrollRequest request);
    }
}
