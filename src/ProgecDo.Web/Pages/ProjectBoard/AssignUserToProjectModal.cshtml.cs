using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProgecDo.ProjectBoard;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ProjectBoard
{
    public class AssignUserToProjectModal : ProgecDoPageModel
    {
        [BindProperty] public AssignUserToProjectViewModel AssignUserViewModel { get; set; }

        private readonly IProjectBoardAppService _projectBoardAppService;
        public List<SelectListItem> Users { get; set; }
 
        public AssignUserToProjectModal(IProjectBoardAppService projectBoardAppService)
        {
            _projectBoardAppService = projectBoardAppService;
        }

        public async Task OnGetAsync(Guid projectId)
        {
            AssignUserViewModel = new AssignUserToProjectViewModel();
            var userLookup = await _projectBoardAppService.GetUserLookupAsync();
            Users = userLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList(); 
            AssignUserViewModel.ProjectId = projectId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectBoardAppService.AssignUserToProject(AssignUserViewModel.ProjectId, AssignUserViewModel.UserId);
     
            return NoContent();
        }

        public class AssignUserToProjectViewModel
        {
            [HiddenInput] public Guid ProjectId { get; set; }

            [SelectItems(nameof(AssignUserToProjectModal.Users))]
            [DisplayName("Users")]
            public Guid UserId { get; set; }
        }
    }
}