using System;
using System.Collections.Generic;
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
            [HiddenInput] public Guid ParentId { get; set; }

            [HiddenInput] public Guid Id { get; set; }

            [DisplayName("DescriptionOfToDoItem")] public string Description { get; set; }

            [DisplayName("NoteOfToDoItem")]
            [TextArea]
            public string Note { get; set; }

            [DisplayName("DueOn")]
            [DataType(DataType.Date)]
            public DateTime? DueDate { get; set; }

            public int Order { get; set; }
            public bool IsDone { get; set; }

            public List<ToDoItemUserViewModel> ToDoItemUsers { get; set; }

            [HiddenInput] public Guid ProjectId { get; set; }
            [HiddenInput] public string ProjectTitle { get; set; }

            [HiddenInput] public string ProjectDescription { get; set; }
            public string ToDoListName { get; set; }
        }

        public class ToDoItemUserViewModel
        {
            public Guid ToDoItemId { get; set; }
            public Guid UserId { get; set; }
            public DateTime CreationTime { get; set; }

            public AppUserViewModel User { get; set; }
        }

        public class AppUserViewModel
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}