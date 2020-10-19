using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgecDo.ToDos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.ToDos
{
    public class EditToDoItemModal : ProgecDoPageModel
    {
        [BindProperty] public CreateUpdateToDoItemViewModel CreateUpdateToDoItem { get; set; }

        private readonly IToDoAppService _toDoAppService;

        public EditToDoItemModal(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGetAsync(Guid toDoListId, Guid toDoItemId)
        {
            CreateUpdateToDoItem = ObjectMapper.Map<ToDoItemDto, CreateUpdateToDoItemViewModel>(await _toDoAppService.GetToDoItemById(toDoListId, toDoItemId));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _toDoAppService.UpdateToDoItemAsync(ObjectMapper.Map<CreateUpdateToDoItemViewModel, CreateUpdateToDoItemDto>(CreateUpdateToDoItem));

            return NoContent();
        }

        public class CreateUpdateToDoItemViewModel
        {
            [HiddenInput] public Guid Id { get; set; }

            [Required]
            [StringLength(ToDoConsts.MaxToDoItemDescriptionLength, MinimumLength = ToDoConsts.MinToDoItemDescriptionLength)]
            [DisplayName("DescriptionOfToDoItem")]
            public string Description { get; set; }

            [DisplayName("NoteOfToDoItem")]
            [StringLength(ToDoConsts.MaxToDoItemNoteLength)]
            [TextArea]
            public string Note { get; set; }

            [DisplayName("DueOn")]
            [DataType(DataType.Date)]
            public DateTime? DueDate { get; set; }

            [HiddenInput] public Guid ParentId { get; set; }
        }
    }
}