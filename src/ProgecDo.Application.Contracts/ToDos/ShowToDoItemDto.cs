using System;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ToDos
{
    public class ShowToDoItemDto : AuditedEntityDto<Guid>
    {
        public Guid ParentId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        
        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
    }
}