using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using ProgecDo.ToDos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class ShowToDoItem : ProgecDoPageModel
    {
        [BindProperty] 
        public ToDoItemViewModel ToDoItem { get; set; }
        
        private readonly IToDoAppService _toDoAppService;

        public ShowToDoItem(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGet(Guid toDoListId, Guid toDoItemId)
        {
            ToDoItem = ObjectMapper.Map<ShowToDoItemDto, ToDoItemViewModel>(await _toDoAppService.GetToDoItemById(toDoListId, toDoItemId));
        }
        
        public class ToDoItemViewModel
        {
            [HiddenInput] 
            public Guid Id { get; set; }
            
            // [Required] 
            // [StringLength(ToDoConsts.MaxNameLength, MinimumLength = ToDoConsts.MinNameLength)]
            // [DisplayName("NameOfToDoList")]
            // public string Name { get; set; }

            [DisplayName("DescriptionOfToDoItem")] 
            // [StringLength(ToDoConsts.MaxToDoItemDescriptionLength)]
            [TextArea]
            public string Description { get; set; }
            
            [DisplayName("DueOn")] 
            [DataType(DataType.Date)]
            public DateTime? DueDate { get; set; }

            [HiddenInput] 
            public Guid ParentId { get; set; }
            
            [HiddenInput] 
            public Guid ProjectId { get; set; }
            [HiddenInput] 
            public string ProjectTitle { get; set; }
            
            [HiddenInput] 
            public string ProjectDescription { get; set; }
        }
    }
}