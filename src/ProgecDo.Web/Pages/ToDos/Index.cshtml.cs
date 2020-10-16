using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using ProgecDo.ToDos;

namespace ProgecDo.Web.Pages.ToDos
{
    public class Index : ProgecDoPageModel
    {
        public ToDoListViewModel ToDos { get; set; }
        private readonly IToDoAppService _toDoAppService;

        public Index(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGetAsync(Guid projectId)
        {
            ToDos = ObjectMapper.Map<ToDoListDto, ToDoListViewModel>(await _toDoAppService.GetToDoListByProjectId(projectId));
        }


        public class ToDoListViewModel
        {
            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public List<ToDoViewModel> ToDo { get; set; }
        }

        public class ToDoViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<ToDoItemViewModel> ToDoItems { get; set; }
            
            // public Guid ProjectId { get; set; }
            // public string ProjectTitle { get; set; }
            // public string ProjectDescription { get; set; }
            public string ToDoListCreatorUserName { get; set; }
            public string ToDoListCreatorName { get; set; }
            public string ToDoListCreatorSurname { get; set; }
            public DateTime CreationTime { get; set; } 
        }
        
        public class ToDoItemViewModel  
        {
            public Guid ParentId { get; set; }
            public string Description { get; set; }
            public DateTime? DueDate { get; set; }
        }
    }
}