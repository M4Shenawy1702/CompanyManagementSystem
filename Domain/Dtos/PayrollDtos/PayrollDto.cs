public class PayrollDto
{
    public int Id { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
    public decimal TotalSalary { get; set; }
    public DateTime PaymentDate { get; set; }
    public string EmployeeId { get; set; } = null!;
}
