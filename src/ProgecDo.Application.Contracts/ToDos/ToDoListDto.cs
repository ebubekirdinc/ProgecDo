using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ToDos
{
    public class ToDoListDto : AuditedEntityDto<Guid>
    {
        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public List<ToDoListWithToDoItemsDto> ToDoListWithToDoItems { get; set; }
    }

    public class ToDoListWithToDoItemsDto
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