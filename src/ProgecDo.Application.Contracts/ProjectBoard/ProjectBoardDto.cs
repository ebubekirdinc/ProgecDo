using System;
using ProgecDo.BoardMessages;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ProjectBoard
{
    public class ProjectBoardDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        public BoardMessageListDto BoardMessageDto { get; set; }
    }
}