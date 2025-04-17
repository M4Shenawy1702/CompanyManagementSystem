namespace Domain.Dtos.PayrollDtos
{
    public class PayrollPaymentDto
    {
        public string EmployeeId { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
    }
}
