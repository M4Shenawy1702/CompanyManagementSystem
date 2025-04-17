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
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public decimal PaymentAmount { get; set; }

        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }


    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }
}
