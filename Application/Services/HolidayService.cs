using AutoMapper;
using Domain.Dtos.Holiday;
using Domain.Entities;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HolidayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HolidayResultDto>> GetAllHolidays()
        {
            var holidays = await _unitOfWork.Holidays.GetAllAsync();
            var holidayResultDtos = _mapper.Map<IEnumerable<HolidayResultDto>>(holidays);
            return holidayResultDtos;
        }

        public async Task<HolidayResultDto> AddHoliday(HolidayDto dto)
        {
            if (await _unitOfWork.Holidays.FindAsync(h => h.Name == dto.Name && !h.IsDeleted) is not null)
                throw new ServiceException(400, "Holiday with the same name already exists.");

            var holiday = _mapper.Map<Holiday>(dto);
            var result = await _unitOfWork.Holidays.InsertAsync(holiday);
            await _unitOfWork.CompleteAsync();

            var holidayResultDto = _mapper.Map<HolidayResultDto>(holiday);
            return holidayResultDto;
        }

        public async Task<HolidayResultDto> GetHolidayById(int holidayId)
        {
            var holiday = await _unitOfWork.Holidays.GetByIdAsync(holidayId)
                          ?? throw new NotFoundException("Holiday not found by id.", "HOLIDAY_NOT_FOUND");

            var holidayResultDto = _mapper.Map<HolidayResultDto>(holiday);
            return holidayResultDto;
        }

        public async Task<HolidayResultDto> GetHolidayByName(string holidayName)
        {
            var holiday = await _unitOfWork.Holidays.FindAsync(h => h.Name == holidayName)
                          ?? throw new NotFoundException("Holiday not found by name.", "HOLIDAY_NOT_FOUND");

            var holidayResultDto = _mapper.Map<HolidayResultDto>(holiday);
            return holidayResultDto;
        }

        public async Task<HolidayResultDto> ToggleHoliday(int holidayId)
        {
            var holiday = await _unitOfWork.Holidays.GetByIdAsync(holidayId)
                          ?? throw new NotFoundException("Holiday not found to toggle.", "HOLIDAY_NOT_FOUND");

            holiday.IsDeleted = !holiday.IsDeleted;
            holiday.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            var holidayResultDto = _mapper.Map<HolidayResultDto>(holiday);
            return holidayResultDto;
        }

        public async Task<HolidayResultDto> UpdateHoliday(HolidayDto dto, int holidayId)
        {
            var holiday = await _unitOfWork.Holidays.GetByIdAsync(holidayId)
                          ?? throw new NotFoundException("Holiday not found for update.", "HOLIDAY_NOT_FOUND");

            holiday.StartDate = dto.StartDate;
            holiday.EndDate = dto.EndDate;
            holiday.Description = dto.Description;
            holiday.Name = dto.Name;

            holiday.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();

            var holidayResultDto = _mapper.Map<HolidayResultDto>(holiday);
            return holidayResultDto;
        }
    }
}
