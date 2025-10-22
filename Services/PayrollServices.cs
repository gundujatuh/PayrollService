using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayrollService.Data;
using PayrollService.Models;

namespace PayrollService.Services
{
    public class PayrollServices : IPayrollService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly decimal _defaultTaxRate;
        private readonly decimal _overtimeMultiplier;
        private readonly int _standardHours;

        public PayrollServices(AppDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;

            _defaultTaxRate = Convert.ToDecimal(_configuration["PayrollSettings:DefaultTaxRate"] ?? "0.1");
            _overtimeMultiplier = Convert.ToDecimal(_configuration["PayrollSettings:OvertimeMultiplier"] ?? "1.5");
            _standardHours = Convert.ToInt32(_configuration["PayrollSettings:StandardHoursPerMonth"] ?? "160");
        }

        public IEnumerable<Employee> GetAllEmployees() => _db.Employees.AsNoTracking().ToList();

        public Employee? GetEmployee(int id) => _db.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);

        public PayrollResult CalculatePayroll(PayrollRequest request)
        {
            var emp = GetEmployee(request.EmployeeId);
            if (emp == null)
                throw new KeyNotFoundException($"Employee with id {request.EmployeeId} not found");

            var taxRate = request.TaxRate.HasValue ? request.TaxRate.Value : _defaultTaxRate;

            decimal overtimePay = 0m;
            if (emp.HoursWorked > _standardHours)
            {
                var overtimeHours = emp.HoursWorked - _standardHours;
                var hourlyRate = emp.BaseSalary / _standardHours;
                overtimePay = hourlyRate * overtimeHours * _overtimeMultiplier;
            }

            var gross = emp.BaseSalary + overtimePay + request.Base;
            var taxAmount = Math.Round(gross * taxRate, 2);
            var net = Math.Round(gross - taxAmount, 2);

            var note = overtimePay > 0 ? $"Overtime applied: {overtimePay}" : "No overtime";

            emp.BaseSalary += request.Base;
            _db.Employees.Update(emp);
            _db.SaveChanges();

            return new PayrollResult
            {
                EmployeeId = emp.Id,
                GrossSalary = Math.Round(gross, 2),
                TaxAmount = taxAmount,
                NetSalary = net,
                Note = note
            };
        }
    }
}