using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using ProgecDo.ToDos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class CreateToDoItemModal : ProgecDoPageModel
    {    
        [BindProperty] 
        public NewToDoItemViewModel NewToDoItem { get; set; }
        
        private readonly IToDoAppService _toDoAppService;

        public CreateToDoItemModal(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public void OnGet(Guid toDoListId)
        {
            NewToDoItem = new NewToDoItemViewModel
            {
                ParentId = toDoListId
            };
        }
        
              
        public async Task<IActionResult> OnPostAsync()
        {
            await _toDoAppService.AddToDoItem(ObjectMapper.Map<NewToDoItemViewModel, CreateUpdateToDoItemDto>(NewToDoItem));
            
            return NoContent();
        }
        
        public class NewToDoItemViewModel
        {
            // [Required] 
            // [StringLength(ToDoConsts.MaxNameLength, MinimumLength = ToDoConsts.MinNameLength)]
            // [DisplayName("NameOfToDoList")]
            // public string Name { get; set; }

            [DisplayName("DescriptionOfToDoItem")] 
            [StringLength(ToDoConsts.MaxToDoItemDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            
            [DisplayName("DueOn")] 
            [DataType(DataType.Date)]
            public DateTime? DueDate { get; set; }

            [HiddenInput] 
            public Guid ParentId { get; set; }
        }
    }
}