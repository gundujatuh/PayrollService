namespace PayrollService.Models
{
    public class PayrollRequest
    {
        public int EmployeeId { get; set; }
        public decimal Base { get; set; } = 0m;
        public decimal? TaxRate { get; set; }
    }
}
