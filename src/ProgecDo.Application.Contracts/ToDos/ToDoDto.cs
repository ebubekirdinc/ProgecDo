using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ToDos
{
    public class ToDoDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public List<ToDoItemDto> ToDoItems { get; set; }
    }

    public class ToDoItemDto : AuditedEntityDto<Guid>
    {
        public Guid ParentId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}