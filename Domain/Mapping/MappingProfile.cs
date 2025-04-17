using AutoMapper;
using Domain.Dtos.Department;
using Domain.Dtos.DepartmentDots;
using Domain.Dtos.EmployeeDto;
using Domain.Dtos.Holiday;
using Domain.Dtos.ProjeectDots;
using Domain.Entities;

namespace Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<ApplicationUser, UserResultDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Id : 0))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.DepartmentId : null))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            // Department
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentResultDto>();

            CreateMap<Department, DepartmentResultDto>()
            .ForMember(dest => dest.EmployeesUsernames,
                opt => opt.MapFrom(src =>
                    src.Employees.Select(e => e.User.UserName).ToList()));


            // Project
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectResultDto>();

            // Holiday
            CreateMap<HolidayDto, Holiday>();
            CreateMap<Holiday, HolidayResultDto>();

            // Payroll
            CreateMap<Payroll, PayrollDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<PayrollDto, Payroll>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(dto => dto.EmployeeId));
        }
    }
}
