namespace Domain.Dtos.PayrollDtos
{
    public class PayrollPaymentDto
    {
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
    }
}
