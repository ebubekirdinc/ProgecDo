using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.BoardMessages
{
    public class BoardMessageListDto : AuditedEntityDto<Guid>
    {
        // public string Title { get; set; }
        // public string Content { get; set; }
        //
        // public string UserName { get; set; }
        // public string UserSurname { get; set; } 
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public List<BoardMessageWithUserDto> BoardMessageList { get; set; }
    }

    public class BoardMessageWithUserDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}