using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.BoardMessages
{
    public class BoardMessageDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public List<CommentDto> Comments { get; set; }

        public Guid ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public int CommentCount { get; set; }
    }

    public class CommentDto : AuditedEntityDto<Guid>
    {
        public Guid ParentId { get; set; }
        public string Content { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}