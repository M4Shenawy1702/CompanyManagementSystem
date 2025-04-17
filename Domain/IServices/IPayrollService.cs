using Domain.Dtos.PayrollDtos;

namespace Domain.IServices
{
    public interface IPayrollService
    {
        Task<PayrollDto> CreatePayroll(PayrollPaymentDto dto);
        Task<IEnumerable<PayrollDto>> GetAllPayrolls();
        Task<PayrollDto> GetPayrollById(int id);
        Task DeletePayroll(int id);
    }
}
