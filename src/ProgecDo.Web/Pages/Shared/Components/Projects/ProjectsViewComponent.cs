using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgecDo.Projects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ProgecDo.Web.Pages.Shared.Components.Projects
{
    public class ProjectsViewComponent : AbpViewComponent
    {
        // public ProjectsViewModel Projects { get; set; }
        private readonly IProjectAppService _projectAppService;

        public ProjectsViewComponent(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projectsList = new ProjectsViewModel
            {
                Projects = await _projectAppService.GetProjectsWithUsersAsync()
            };

            return View("~/Pages/Shared/Components/Projects/Projects.cshtml", projectsList);
        }

        public class ProjectsViewModel
        {
            // public PagedResultDto<ProjectDto> Pdto { get; set; } 
            public ListResultDto<ProjectDto> Projects { get; set; }
        }
    }
}

