using Domain.Dtos.PayrollDtos;
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
    public async Task<ActionResult<PayrollDto>> CreatePayrollSession([FromBody] PayrollPaymentDto dto)
    {
        var result = await _payrollService.CreatePayroll(dto);

        return Ok(result);
    }
    [HttpGet("payroll/{id}")]
    public async Task<ActionResult<PayrollDto>> GetPayrollById(int id)
    {
        var result = await _payrollService.GetPayrollById(id);

        return Ok(result);
    }
    [HttpDelete("payroll/{id}")]
    public async Task<ActionResult<PayrollDto>> DeletePayroll(int id)
    {
        var result = _payrollService.DeletePayroll(id);

        return Ok(result);
    }
    [HttpGet("payrolls")]
    public async Task<ActionResult<IEnumerable<PayrollDto>>> GetAllPayrolls()
    {
        var result = await _payrollService.GetAllPayrolls();

        return Ok(result);
    }
}
