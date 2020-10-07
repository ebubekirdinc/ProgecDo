using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgecDo.ToDos;

namespace ProgecDo.Web.Pages.ToDos
{
    public class Index : ProgecDoPageModel
    {
        public ToDosViewModel ToDos { get; set; }
        private readonly IToDoAppService _toDoAppService;

        public Index(IToDoAppService toDoAppService)
        {
            _toDoAppService = toDoAppService;
        }

        public async Task OnGetAsync(Guid projectId)
        {
            ToDos = ObjectMapper.Map<ToDoListDto, ToDosViewModel>(await _toDoAppService.GetToDoListByProjectId(projectId));
        }


        public class ToDosViewModel
        {
            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public List<ToDoListWithToDoItemsViewModel> ToDoListWithToDoItems { get; set; }
        }

        public class ToDoListWithToDoItemsViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string ToDoListCreatorUserName { get; set; }
            public string ToDoListCreatorName { get; set; }

            public string ToDoListCreatorSurname { get; set; }

            // public int CommentCount { get; set; }
            public DateTime CreationTime { get; set; }
        
            // public List<ToDoItemDto> ToDoItemList { get; set; }
        }
    }
}