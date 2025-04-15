using AutoMapper;
using Domain.Dtos.AuthDots;
using Domain.Dtos.Department;
using Domain.Dtos.DepartmentDots;
using Domain.Dtos.EmailDto;
using Domain.Dtos.EmployeeDto;
using Domain.Dtos.Holiday;
using Domain.Dtos.PayrollDtos;
using Domain.Dtos.ProjeectDots;
using Domain.Entities;

namespace Bookify.Web.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Department
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentResultDto>();

            // Project
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectResultDto>();

            // Employee
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeResultDto>();
            CreateMap<RegisterDto, Employee>();
            CreateMap<UpdateInfoDto, Employee>();

            // Holiday
            CreateMap<HolidayDto, Holiday>();
            CreateMap<Holiday, HolidayResultDto>();

            // Email
            CreateMap<EmailDto, Email>();
            CreateMap<Email, EmailResultDto>();

            // Payroll
            CreateMap<Payroll, PayrollDto>();
        }
    }
}
