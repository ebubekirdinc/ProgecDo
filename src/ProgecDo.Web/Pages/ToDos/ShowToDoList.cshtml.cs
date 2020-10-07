using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgecDo.ToDos;

namespace ProgecDo.Web.Pages.ToDos
{
    public class ShowToDoList : ProgecDoPageModel
    {
        public ShowToDoListViewModel ToDoList { get; set; }
        private readonly IToDoAppService _toDoAppService;

        public ShowToDoList(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGet(Guid toDoListId)
        {
            ToDoList = ObjectMapper.Map<ToDoDto, ShowToDoListViewModel>(await _toDoAppService.GetToDoListWithToDoItemsByToDoListId(toDoListId));
        }

        public class ShowToDoListViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string ToDoListCreatorUserName { get; set; }
            public string ToDoListCreatorName { get; set; }
            public string ToDoListCreatorSurname { get; set; }
            
            public DateTime CreationTime { get; set; }

            public List<ToDoItemViewModel> ToDoItemList { get; set; }

            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }

            public string ProjectDescription { get; set; }
            // public int CommentCount { get; set; }
        }

        public class ToDoItemViewModel
        {
            public Guid Id { get; set; }

            public virtual Guid ParentId { get; set; }
            public virtual string Description { get; set; }
            public virtual DateTime? DueDate { get; set; }

            // public string UserName { get; set; }
            // public string Name { get; set; }
            // public string Surname { get; set; }
            // public DateTime CreationTime { get; set; }
        }
    }
}