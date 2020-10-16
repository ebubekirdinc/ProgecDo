using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProgecDo.ToDos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class AssignUserToToDoItemModal : ProgecDoPageModel
    {
        [BindProperty] public AssignUserToToDoItemViewModel AssignUserViewModel { get; set; }

        private readonly IToDoAppService _toDoAppService;

        public AssignUserToToDoItemModal(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public List<SelectListItem> Users { get; set; }

        public async Task OnGetAsync(Guid toDoItemId)
        {
            AssignUserViewModel = new AssignUserToToDoItemViewModel();
            var userLookup = await _toDoAppService.GetUserLookupAsync();
            Users = userLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList(); 
            AssignUserViewModel.ToDoItemId = toDoItemId;
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _toDoAppService.AssignUserToProject(AssignUserViewModel.ToDoItemId, AssignUserViewModel.UserId);
     
            return NoContent();
        }

        public class AssignUserToToDoItemViewModel
        {
            [HiddenInput] public Guid ToDoItemId { get; set; }

            [SelectItems(nameof(Users))]
            [DisplayName("Users")]
            public Guid UserId { get; set; }
        }
    }
}