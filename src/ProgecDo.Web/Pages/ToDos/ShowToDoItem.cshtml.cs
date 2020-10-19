using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgecDo.ToDos;
using ProgecDo.Web.Models;

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
            ToDoItem = ObjectMapper.Map<ToDoItemDto, ToDoItemViewModel>(await _toDoAppService.GetToDoItemById(toDoListId, toDoItemId));
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

        public class ToDoItemUserViewModel : EntityViewModel
        {
            public Guid ToDoItemId { get; set; }
            public Guid UserId { get; set; }
            public DateTime CreationTime { get; set; }

            public Index.AppUserViewModel User { get; set; }
        }

        public class AppUserViewModel : FullAuditedEntityViewModel
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}