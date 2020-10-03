using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProgecDo.Projects
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Title { get; private set; }
        public virtual string Description { get; set; }

        public virtual List<ProjectUser> ProjectUsers { get; protected set; }

        public Project()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Project(Guid id, [NotNull] string title, string description, Guid creatorUserId) : base(id)
        {
            SetTitle(title);
            Description = description;

            ProjectUsers = new List<ProjectUser>
            {
                new ProjectUser(id, creatorUserId)
            };
        }

        private void SetTitle([NotNull] string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(Title), ProjectConsts.MaxTitleLength, ProjectConsts.MinTitleLength);
        }

        public void AssignUserToProject(Guid userId)
        {
            if (ProjectUsers.Any(x => x.UserId == userId))
            {
                throw new UserAlreadyAssignedToProjectException();
            }
            else
            {
                ProjectUsers.Add(new ProjectUser(Id, userId)); 
            }
        }
    }
}