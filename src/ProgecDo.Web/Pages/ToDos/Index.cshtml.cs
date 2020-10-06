using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgecDo.ToDos;

namespace ProgecDo.Web.Pages.ToDos
{
    public class Index : PageModel
    {
        public ToDosViewModel ToDos { get; set; }
        private readonly IToDoAppService _toDoAppService;

        public Index(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public void OnGet(Guid projectId)
        {
            ToDos.ProjectId = projectId;
        }

        public class ToDosViewModel
        {
            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public List<ToDoItemViewModel> ToDoItemList { get; set; }
        }

        public class ToDoItemViewModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public int CommentCount { get; set; }
            public DateTime CreationTime { get; set; }
        }
    }
}