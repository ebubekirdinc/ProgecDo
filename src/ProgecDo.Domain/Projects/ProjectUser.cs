using System;
using ProgecDo.Users;
using Volo.Abp.Domain.Entities;

namespace ProgecDo.Projects
{
    public class ProjectUser : Entity
    {
        public virtual Guid ProjectId { get; private set; }
        public virtual Guid UserId { get; private set; }
        public DateTime CreationTime { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Project Project { get; set; }

        protected ProjectUser()
        {
        }

        internal ProjectUser(Guid projectId, Guid userId)
        {
            ProjectId = projectId;
            UserId = userId;
            CreationTime = DateTime.Now; 
        }

        public override object[] GetKeys()
        {
            return new object[] {ProjectId, UserId};
        }
    }
}