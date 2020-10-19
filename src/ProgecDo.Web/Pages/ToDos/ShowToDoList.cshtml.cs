using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgecDo.ToDos;
using ProgecDo.Web.Models;

namespace ProgecDo.Web.Pages.ToDos
{
    public class ShowToDoList : ProgecDoPageModel
    {
        public ToDoViewModel ToDoList { get; set; }
        private readonly IToDoAppService _toDoAppService;

        public ShowToDoList(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGet(Guid toDoListId)
        { 
            ToDoList = ObjectMapper.Map<ToDoDto, ToDoViewModel>(await _toDoAppService.GetToDoListWithToDoItemsByToDoListId(toDoListId));
        }

        public class ToDoViewModel : AuditedEntityViewModel<Guid>
        { 
            public string Name { get; set; }
            public string Description { get; set; }
            public List<ToDoItemViewModel> ToDoItems { get; set; }

            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public string ToDoListCreatorUserName { get; set; }
            public string ToDoListCreatorName { get; set; }
            public string ToDoListCreatorSurname { get; set; } 
        }

        public class ToDoItemViewModel : AuditedEntityViewModel<Guid>
        {
            public Guid ParentId { get; set; }
            public string Description { get; set; }
            public string Note { get; set; }
            public DateTime? DueDate { get; set; }
            public int Order { get; set; }
            public bool IsDone { get; set; }

            public List<Index.ToDoItemUserViewModel> ToDoItemUsers { get; set; }

            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public string ToDoListName { get; set; } 
        }
    }
}