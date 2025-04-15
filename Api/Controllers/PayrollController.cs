using Domain.Dtos.PayrollDtos;
using Domain.Errors;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPayrollService _payrollService;

    public PaymentController(IPayrollService payrollService)
    {
        _payrollService = payrollService;
    }

    [HttpPost("payroll")]
    public async Task<IActionResult> CreatePayrollSession([FromBody] PayrollPaymentDto dto)
    {
        try
        {
            var sessionUrl = await _payrollService.CreateCheckoutSession(dto);
            return Ok(new { Url = sessionUrl });
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new
            {
                ex.Message,
                ex.ErrorCode,
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while creating the session.",
                Error = ex.Message
            });
        }
    }

    [HttpGet("success")]
    public async Task<IActionResult> Success([FromQuery] string sessionId, [FromQuery] int id)
    {
        try
        {
            var result = await _payrollService.CheckoutSuccess(sessionId, id);
            return Ok(new { Message = result, SessionId = sessionId });
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new
            {
                ex.Message,
                ex.ErrorCode
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred during success handling.",
                Error = ex.Message
            });
        }
    }

    [HttpGet("failed")]
    public async Task<IActionResult> Failed([FromQuery] string sessionId, [FromQuery] int id)
    {
        try
        {
            var result = await _payrollService.CheckoutFail(sessionId, id);
            return BadRequest(new { Message = result, SessionId = sessionId });
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new
            {
                ex.Message,
                ex.ErrorCode
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred during failure handling.",
                Error = ex.Message
            });
        }
    }

    [HttpGet("payrolls")]
    public async Task<IActionResult> GetAllPayrolls()
    {
        try
        {
            var payrolls = await _payrollService.GetAllPayrollsAsync();
            return Ok(payrolls);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new
            {
                ex.Message,
                ex.ErrorCode
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while fetching payrolls.",
                Error = ex.Message
            });
        }
    }
}
