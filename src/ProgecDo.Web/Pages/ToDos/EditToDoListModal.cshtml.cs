using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using ProgecDo.ToDos; 
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class EditToDoListModal : ProgecDoPageModel
    {
        [BindProperty] 
        public EditToDoListViewModel EditToDoList { get; set; }

        private readonly IToDoAppService _toDoAppService;

        public EditToDoListModal(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGetAsync(Guid toDoListId)
        { 
            var toDoDto = await _toDoAppService.GetAsync(toDoListId);

            EditToDoList = ObjectMapper.Map<ToDoDto, EditToDoListViewModel>(toDoDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createUpdateToDoDto = ObjectMapper.Map<EditToDoListViewModel, CreateUpdateToDoDto>(EditToDoList);
            
            await _toDoAppService.UpdateAsync(EditToDoList.Id, createUpdateToDoDto);

            return NoContent();
        }
        
        public class EditToDoListViewModel
        {
            [HiddenInput] 
            public Guid Id { get; set; }

            [Required]
            [StringLength(ToDoConsts.MaxNameLength, MinimumLength = ToDoConsts.MinNameLength)]
            [DisplayName("NameOfToDoList")]
            public string Name { get; set; }

            [DisplayName("DescriptionOfToDoList")]
            [StringLength(ToDoConsts.MaxDescriptionLength)]
            [TextArea]
            public string Description { get; set; }

            [HiddenInput] 
            public Guid ProjectId { get; set; } 
        }
    }
}