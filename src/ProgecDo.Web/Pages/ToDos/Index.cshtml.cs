using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using ProgecDo.ToDos;
using ProgecDo.Web.Models; 

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


        public class ToDoListViewModel : AuditedEntityViewModel<Guid>
        {
            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public List<ToDoViewModel> ToDos { get; set; }
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

            public List<ToDoItemUserViewModel> ToDoItemUsers { get; set; }

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

            public AppUserViewModel User { get; set; }
        }

        public class AppUserViewModel : FullAuditedEntityViewModel
        {
            public string UserName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}