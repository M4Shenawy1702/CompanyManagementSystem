using Domain.Dtos.Department;
using Domain.Dtos.ProjeectDots;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("Addproject")]
        public async Task<ActionResult<ProjectResultDto>> Addproject([FromForm] ProjectDto Dto)
        {
            var project = await _projectService.AddProject(Dto);

            return Ok(project);
        }
        [HttpGet("GetAllproject")]
        public async Task<ActionResult<IEnumerable<ProjectResultDto>>> GetAllprojects()
        {
            var projects = await _projectService.GetAllProjects();

            return Ok(projects);
        }
        [HttpGet("GetprojectById/{depId}")]
        public async Task<ActionResult<ProjectResultDto>> GetprojectById(int depId)
        {
            var project = await _projectService.GetProjectById(depId);

            return Ok(project);
        }
        [HttpGet("GetprojectByName/{depName}")]
        public async Task<ActionResult<ProjectResultDto>> GetprojectByName(string depName)
        {
            var project = await _projectService.GetProjectByName(depName);

            return Ok(project);
        }
        [HttpPut("ToggleProjectStatus/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> ToggleProjectStatus(int Id)
        {
            var result = await _projectService.ToggleProjectStatus(Id);

            return Ok(result);
        }
        [HttpPut("ToggleProject/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> ToggleProject(int Id)
        {
            var result = await _projectService.ToggleProject(Id);

            return Ok(result);
        }
        [HttpPut("UpdateProject/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> UpdateProject([FromForm] ProjectDto Dto, int Id)
        {
            var project = await _projectService.UpdateProject(Dto, Id);

            return Ok(project);
        }
        [HttpPut("ChangeProjManager/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> ChangeProjManager([FromForm] Emp_ProjDto Dto, int Id)
        {
            var project = await _projectService.ChangeProjectManager(Dto, Id);

            return Ok(project);
        }
        [HttpPut("AddEmpToDept/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> AddEmpToDept([FromForm] Emp_ProjDto Dto, int Id)
        {
            var project = await _projectService.AddEmpToProj(Dto, Id);

            return Ok(project);
        }
        [HttpPut("RemoveEmpFromDept/{Id}")]
        public async Task<ActionResult<ProjectResultDto>> RemoveEmpFromDept([FromForm] Emp_ProjDto Dto, int Id)
        {
            var project = await _projectService.RemoveEmpFromProj(Dto, Id);

            return Ok(project);
        }
    }
}
