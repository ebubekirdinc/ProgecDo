using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.Projects
{
    [Serializable]
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ProjectUserDto> ProjectUsers { get; set; }  
    }
}