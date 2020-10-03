using System;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.Projects
{
    public class ProjectUserDto : EntityDto
    {
        public virtual Guid ProjectId { get; set; }
        public virtual Guid UserId { get; set; }

        public virtual AppUserDto User { get; set; }

        public virtual ProjectDto ProjectDto { get; set; }
    }
}