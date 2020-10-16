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
        public List<ToDoDto> ToDo { get; set; }
    }
 
}