namespace Domain.Entities
{
    public class Payroll
    {
        public int Id { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime PaymentDate { get; set; }
        public int EmployeeId { set; get; }
        public virtual Employee? Employee { get; set; }

        public string? StripePaymentIntentId { get; set; }
        public string? StripeCheckoutSessionId { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal PaymentAmount { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
}
