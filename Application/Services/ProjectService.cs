using AutoMapper;
using Domain.Dtos.Department;
using Domain.Dtos.ProjeectDots;
using Domain.Entities;
using Domain.Errors;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectResultDto>> GetAllProjects()
        {
            var projects = await _unitOfWork.Projects.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectResultDto>>(projects);
        }

        public async Task<ProjectResultDto> GetProjectById(int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found by ID.", "PROJECT_NOT_FOUND");

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> GetProjectByName(string projectName)
        {
            var project = await _unitOfWork.Projects.FindAsync(p => p.Name == projectName)
                          ?? throw new NotFoundException("Project not found by name.", "PROJECT_NOT_FOUND");

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> AddProject(ProjectDto dto)
        {
            if (await _unitOfWork.Projects.FindAsync(d => d.ManagerId == dto.ManagerId && !d.IsDeleted) is not null)
                throw new ServiceException(400, "The manager is already assigned to an existing project.");

            var project = _mapper.Map<Project>(dto);
            await _unitOfWork.Projects.InsertAsync(project);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> ToggleProjectStatus(int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found.", "PROJECT_NOT_FOUND");

            project.Status = !project.Status;
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> ToggleProject(int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found.", "PROJECT_NOT_FOUND");

            project.IsDeleted = !project.IsDeleted;
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> UpdateProject(ProjectDto dto, int projectId)
        {
            if (await _unitOfWork.Projects.FindAsync(d => d.ManagerId == dto.ManagerId && !d.IsDeleted) is not null)
                throw new ServiceException(400, "Manager is already assigned to an existing project.");

            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found for update.", "PROJECT_NOT_FOUND");

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.ManagerId = dto.ManagerId;

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> ChangeProjectManager(Emp_ProjDto dto, int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found.", "PROJECT_NOT_FOUND");

            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmpId)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (project.ManagerId == employee.Id)
                throw new ServiceException(400, "Employee is already the project manager.");

            project.ManagerId = dto.EmpId;
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> AddEmpToProj(Emp_ProjDto dto, int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found.", "PROJECT_NOT_FOUND");

            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmpId)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (project.Employees!.Contains(employee))
                throw new ServiceException(400, "Employee is already assigned to the project.");

            project.Employees.Add(employee);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }

        public async Task<ProjectResultDto> RemoveEmpFromProj(Emp_ProjDto dto, int projectId)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(projectId)
                          ?? throw new NotFoundException("Project not found.", "PROJECT_NOT_FOUND");

            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmpId)
                           ?? throw new NotFoundException("Employee not found.", "EMPLOYEE_NOT_FOUND");

            if (!project.Employees!.Contains(employee))
                throw new ServiceException(400, "Employee is not part of the project.");

            project.Employees.Remove(employee);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProjectResultDto>(project);
        }
    }
}
