using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgecDo.Projects;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.Projects
{
    public class EditModalModel : ProgecDoPageModel
    {
        private readonly IProjectAppService _projectAppService;

        public EditModalModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        [BindProperty]
        public EditProjectViewModel Project { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            var projectDto = await _projectAppService.GetAsync(id);
            Project = ObjectMapper.Map<ProjectDto, EditProjectViewModel>(projectDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectAppService.UpdateAsync(Project.Id, ObjectMapper.Map<EditProjectViewModel, CreateUpdateProjectDto>(Project)
            );

            return NoContent();
        }

        public class EditProjectViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(ProjectConsts.MaxTitleLength, MinimumLength = ProjectConsts.MinTitleLength)]
            [DisplayName("NameOfTheProject")]
            public string Title { get; set; }

            [DisplayName("AddABriefDescription")] 
            [TextArea]
            public string Description { get; set; }
        }
    }
}