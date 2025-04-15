using AutoMapper;
using Domain.Dtos.PayrollDtos;
using Domain.Entities;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Settings;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ServiceStack.Stripe.Types;
using Stripe.Checkout;

namespace Application.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StripeSettings _stripeSettings;
        private readonly IMapper _mapper;
        private readonly IServer _server;

        public PayrollService(IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeSettings, IMapper mapper, IServer server)
        {
            _unitOfWork = unitOfWork;
            _stripeSettings = stripeSettings.Value;
            _mapper = mapper;
            _server = server;
        }

        public async Task<string> CreateCheckoutSession(PayrollPaymentDto dto)
        {
            var serverAddressesFeature = _server.Features.Get<IServerAddressesFeature>();
            var thisApiUrl = serverAddressesFeature?.Addresses.FirstOrDefault();

            if (string.IsNullOrEmpty(thisApiUrl))
            {
                throw new ServiceException(StatusCodes.Status500InternalServerError,
                    "Unable to determine the server address for creating the payment session.",
                    "SERVER_ADDRESS_NOT_FOUND");
            }

            return await GeneratePaymentUrl(thisApiUrl, dto);
        }

        public async Task<string> CheckoutSuccess(string sessionId, int id)
        {
            try
            {
                var session = await new SessionService().GetAsync(sessionId);

                if (session?.Metadata == null)
                    throw new ServiceException(StatusCodes.Status400BadRequest, "Invalid session metadata.", "INVALID_SESSION");

                var payroll = ExtractPayrollFromSession(session, PaymentStatus.Paid);

                await _unitOfWork.Payrolls.InsertAsync(payroll);
                await _unitOfWork.CompleteAsync();

                return "Payment successful and payroll recorded.";
            }
            catch (StripeException ex)
            {
                throw new ServiceException(StatusCodes.Status502BadGateway, "Stripe API error.", "STRIPE_ERROR", null, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(StatusCodes.Status500InternalServerError, "Error processing successful payment.", "CHECKOUT_SUCCESS_ERROR", new Dictionary<string, string>
            {
                { "sessionId", sessionId },
                { "exceptionMessage", ex.Message }
            }, ex);
            }
        }

        public async Task<string> CheckoutFail(string sessionId, int id)
        {
            try
            {
                var session = await new SessionService().GetAsync(sessionId);

                if (session?.Metadata == null)
                    throw new ServiceException(StatusCodes.Status400BadRequest, "Invalid session metadata.", "INVALID_SESSION");

                var payroll = ExtractPayrollFromSession(session, PaymentStatus.Failed);

                await _unitOfWork.Payrolls.InsertAsync(payroll);
                await _unitOfWork.CompleteAsync();

                return "Payment failed, payroll recorded as failed.";
            }
            catch (StripeException ex)
            {
                throw new ServiceException(StatusCodes.Status502BadGateway, "Stripe API error.", "STRIPE_ERROR", null, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(StatusCodes.Status500InternalServerError, "Error processing failed payment.", "CHECKOUT_FAIL_ERROR", new Dictionary<string, string>
            {
                { "sessionId", sessionId },
                { "exceptionMessage", ex.Message }
            }, ex);
            }
        }

        public async Task<IEnumerable<PayrollDto>> GetAllPayrollsAsync()
        {
            try
            {
                var payrolls = await _unitOfWork.Payrolls.GetAllAsync();
                return _mapper.Map<IEnumerable<PayrollDto>>(payrolls);
            }
            catch (Exception ex)
            {
                throw new ServiceException(StatusCodes.Status500InternalServerError, "Failed to retrieve payrolls.", "GET_PAYROLLS_ERROR", null, ex);
            }
        }

        private async Task<string> GeneratePaymentUrl(string thisApiUrl, PayrollPaymentDto dto)
        {
            var totalSalary = dto.BaseSalary + dto.Bonus - dto.Deduction;

            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{thisApiUrl}/api/payment/success?sessionId={{CHECKOUT_SESSION_ID}}&id={dto.EmployeeId}",
                CancelUrl = $"{thisApiUrl}/api/payment/failed?sessionId={{CHECKOUT_SESSION_ID}}&id={dto.EmployeeId}",
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmountDecimal = (long)totalSalary * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Payroll for Employee {dto.EmployeeId}"
                        }
                    },
                    Quantity = 1
                }
            },
                Mode = "payment",
                Metadata = new Dictionary<string, string>
            {
                { "EmployeeId", dto.EmployeeId.ToString() },
                { "BaseSalary", dto.BaseSalary.ToString() },
                { "Bonus", dto.Bonus.ToString() },
                { "Deduction", dto.Deduction.ToString() }
            }
            };

            var session = await new SessionService().CreateAsync(options);
            return session.Url;
        }

        private Payroll ExtractPayrollFromSession(Session session, PaymentStatus status)
        {
            try
            {
                var employeeId = int.Parse(session.Metadata["EmployeeId"]);
                var baseSalary = decimal.Parse(session.Metadata["BaseSalary"]);
                var bonus = decimal.Parse(session.Metadata["Bonus"]);
                var deduction = decimal.Parse(session.Metadata["Deduction"]);
                var totalSalary = baseSalary + bonus - deduction;

                return new Payroll
                {
                    EmployeeId = employeeId,
                    BaseSalary = baseSalary,
                    Bonus = bonus,
                    Deduction = deduction,
                    TotalSalary = totalSalary,
                    PaymentDate = DateTime.UtcNow,
                    PaymentStatus = status
                };
            }
            catch (Exception ex)
            {
                throw new ServiceException(StatusCodes.Status400BadRequest, "Failed to parse session metadata into payroll.", "PAYROLL_PARSE_ERROR", null, ex);
            }
        }
    }
}