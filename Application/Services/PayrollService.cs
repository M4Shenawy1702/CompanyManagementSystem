using AutoMapper;
using Domain.Dtos.PayrollDtos;
using Domain.Entities;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Settings;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PayrollService(IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeSettings, IMapper mapper, IServer server)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PayrollDto> CreatePayroll(PayrollPaymentDto dto)
        {
            var employee = await _unitOfWork.Users.FindAsync(e => e.Id == dto.EmployeeId);
            if (employee == null)
                throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            var totalSalary = employee.Salary + dto.Bonus - dto.Deduction;

            var payroll = new Payroll
            {
                BaseSalary = employee.Salary,
                Bonus = dto.Bonus,
                Deduction = dto.Deduction,
                TotalSalary = totalSalary,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Paid,
                UserId = employee.Id,
            };

            await _unitOfWork.Payrolls.InsertAsync(payroll);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<PayrollDto>(payroll);

            return result ?? throw new ServiceException(StatusCodes.Status500InternalServerError, "Payroll not created.", "PAYROLL_CREATION_FAILED");
        }

        public async Task<PayrollDto> GetPayrollById(int id)
        {
            var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
            return _mapper.Map<PayrollDto>(payroll);
        }
        public async Task DeletePayroll(int id)
        {
            var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
            if (payroll == null)
                throw new NotFoundException("Payroll not found.", "PAYROLL_NOT_FOUND");

            _unitOfWork.Payrolls.Delete(payroll);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<PayrollDto>> GetAllPayrolls()
        {
            var payrolls = await _unitOfWork.Payrolls.FindAllWithIncludesAsync(["UserPayrolls.User"]);
            return _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
        }

    }
}