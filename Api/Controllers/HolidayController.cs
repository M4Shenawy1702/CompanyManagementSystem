using Domain.Dtos.EmployeeDto;
using Domain.Dtos.Holiday;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpPost("AddHoliday")]
        public async Task<ActionResult<HolidayResultDto>> AddHoliday([FromForm] HolidayDto Dto)
        {
            var Holiday = await _holidayService.AddHoliday(Dto);

            return Ok(Holiday);
        }
        [HttpGet("GetAllHolidaies")]
        public async Task<ActionResult<IEnumerable<UserResultDto>>> GetAllHoliday()
        {
            var Companies = await _holidayService.GetAllHolidays();

            return Ok(Companies);
        }
        [HttpGet("GetHolidayById/{HolidayId}")]
        public async Task<ActionResult<HolidayResultDto>> GetHolidayById(int HolidayId)
        {
            var Holiday = await _holidayService.GetHolidayById(HolidayId);

            return Ok(Holiday);
        }
        [HttpGet("GetHolidayByName/{HolidayName}")]
        public async Task<ActionResult<HolidayResultDto>> GetHolidayByName(string HolidayName)
        {
            var Holiday = await _holidayService.GetHolidayByName(HolidayName);

            return Ok(Holiday);
        }
        [HttpPut("ToggleHoliday/{HolidayId}")]
        public async Task<ActionResult<HolidayResultDto>> ToggleHoliday(int HolidayId)
        {
            var Holiday = await _holidayService.ToggleHoliday(HolidayId);

            return Ok(Holiday);
        }
        [HttpPut("UpdateHoliday/{HolidayId}")]
        public async Task<ActionResult<HolidayResultDto>> UpdateHoliday([FromForm] HolidayDto Dto, int HolidayId)
        {
            var Holiday = await _holidayService.UpdateHoliday(Dto, HolidayId);

            return Ok(Holiday);
        }
    }
}
