using Domain.Dtos.Department;
using Domain.Dtos.ProjeectDots;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IProjectService
    {
        public Task<IEnumerable<ProjectResultDto>> GetAllProjects();
        public Task<ProjectResultDto> AddProject(ProjectDto Dto);
        public Task<ProjectResultDto> ToggleProject(int projectId);
        public Task<ProjectResultDto> ToggleProjectStatus(int projectId);
        public Task<ProjectResultDto> GetProjectById(int projectId);
        public Task<ProjectResultDto> GetProjectByName(string projectId);
        public Task<ProjectResultDto> UpdateProject(ProjectDto Dto, int projectId);
        public Task<ProjectResultDto> ChangeProjectManager(Emp_ProjDto Dto, int projectId);
        public Task<ProjectResultDto> AddEmpToProj(Emp_ProjDto Dto, int projectId);
        public Task<ProjectResultDto> RemoveEmpFromProj(Emp_ProjDto Dto, int projectId);
    }
}
