using Domain.Dtos.Department;
using Domain.Dtos.Holiday;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IHolidayService
    {
        public Task<HolidayResultDto > AddHoliday(HolidayDto   Dto);
        public Task<HolidayResultDto> ToggleHoliday(int HolidayId);
        public Task<IEnumerable<HolidayResultDto>> GetAllHolidays();
        public Task<HolidayResultDto> GetHolidayById(int HolidayId);
        public Task<HolidayResultDto> GetHolidayByName(string HolidayName);
        public Task<HolidayResultDto> UpdateHoliday(HolidayDto Dto, int HolidayId);

    }
}
