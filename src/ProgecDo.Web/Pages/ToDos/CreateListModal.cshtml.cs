using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;  
using ProgecDo.ToDos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class CreateListModal : ProgecDoPageModel
    {
        [BindProperty] 
        public NewToDoListViewModel NewToDoList { get; set; }
        
        private readonly IToDoAppService _toDoAppService;

        public CreateListModal(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public void OnGet(Guid projectId)
        {
            NewToDoList = new NewToDoListViewModel
            {
                ProjectId = projectId
            };
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _toDoAppService.CreateAsync(ObjectMapper.Map<NewToDoListViewModel, CreateUpdateToDoDto>(NewToDoList));
            
            return NoContent();
        }
        
        public class NewToDoListViewModel
        {
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