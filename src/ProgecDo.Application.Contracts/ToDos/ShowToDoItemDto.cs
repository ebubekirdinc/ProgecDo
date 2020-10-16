using System;
using System.Collections.Generic;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ToDos
{
    public class ShowToDoItemDto : AuditedEntityDto<Guid>
    {
        public Guid ParentId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime? DueDate { get; set; }
        public int Order { get; set; }
        public bool IsDone { get; set; }

        public List<ToDoItemUserDto> ToDoItemUsers { get; set; }


        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ToDoListName { get; set; }
    }

    public class ToDoItemUserDto : EntityDto
    {
        public Guid ToDoItemId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationTime { get; set; }

        public  AppUserDto User { get; set; }
    }
}