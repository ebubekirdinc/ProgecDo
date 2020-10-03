using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using ProgecDo.Projects;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.Projects
{
    public class CreateModalModel : ProgecDoPageModel
    {
        [BindProperty]
        public CreateProjectViewModel Project { get; set; }

        private readonly IProjectAppService _projectAppService;

        public CreateModalModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task OnGetAsync()
        {
            Project = new CreateProjectViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectAppService.CreateAsync(ObjectMapper.Map<CreateProjectViewModel, CreateUpdateProjectDto>(Project));

            return NoContent();
        }

        public class CreateProjectViewModel
        {
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