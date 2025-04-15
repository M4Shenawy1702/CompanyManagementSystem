using Domain.Dtos.PayrollDtos;

namespace Domain.IServices
{
    public interface IPayrollService
    {
        Task<string> CreateCheckoutSession(PayrollPaymentDto dto);
        Task<string> CheckoutSuccess(string sessionId, int id);
        Task<string> CheckoutFail(string sessionId, int id);
        Task<IEnumerable<PayrollDto>> GetAllPayrollsAsync();
    }
}
