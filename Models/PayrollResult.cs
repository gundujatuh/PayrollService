namespace PayrollService.Models
{
    public class PayrollResult
    {
        public int EmployeeId { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetSalary { get; set; }
        public string Currency { get; set; } = "IDR";
        public string Note { get; set; } = string.Empty;
    }
}
