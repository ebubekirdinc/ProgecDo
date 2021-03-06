using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProgecDo.ToDos
{
    public class ToDo : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual Guid ProjectId { get; set; }

        public virtual List<ToDoItem> ToDoItems { get; private set; }

        public ToDo()
        {
        }

        internal ToDo(Guid id, [NotNull] string name, string description, Guid projectId) : base(id)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(Name), ToDoConsts.MaxNameLength, ToDoConsts.MinNameLength);
            Description = description;
            ProjectId = projectId;

            ToDoItems = new List<ToDoItem>
            {
            };
        }

        public void AddToDoItem(string description, DateTime? dueDate)
        {
            Check.NotNullOrWhiteSpace(description, nameof(Description), ToDoConsts.MaxToDoItemDescriptionLength);

            ToDoItems.Add(new ToDoItem(Id, description, dueDate));
        }

        public void DeleteToDoItem(Guid toDoItemId)
        {
            ToDoItems.Remove(ToDoItems.FirstOrDefault(x => x.Id == toDoItemId));
        }
    }

    public class ToDoItem : FullAuditedEntity<Guid>
    {
        public virtual Guid ParentId { get; private set; }
        public virtual string Description { get; private set; }
        public virtual string Note { get; private set; }
        public virtual DateTime? DueDate { get; private set; }
        public virtual int Order { get; private set; }
        public virtual bool IsDone { get; private set; }
        public virtual List<ToDoItemUser> ToDoItemUsers { get; set; }

        public virtual ToDo ToDo { get; set; }

        public ToDoItem()
        {
        }

        internal ToDoItem(Guid parentId, string description, DateTime? dueDate)
        {
            ParentId = parentId;
            SetDescription(description);
            DueDate = dueDate;
        }

        internal ToDoItem UpdatToDoItem(string description, string note, DateTime? dueDate)
        {
            SetDescription(description);
            SetNote(note);
            DueDate = dueDate;

            return this;
        }

        private void SetDescription([NotNull] string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(Description), maxLength: ToDoConsts.MaxToDoItemDescriptionLength);
        }

        private void SetNote(string note)
        {
            Note = note;
        }

        private void SetOrder(int order)
        {
            Order = order;
        }

        private void SetIsDone(bool isDone)
        {
            IsDone = isDone;
        }

        public void AssignUserToToDoItem(Guid userId)
        {
            if (ToDoItemUsers.Any(x => x.UserId == userId))
            {
                throw new UserAlreadyAssignedToToDoItemException();
            }
            else
            {
                ToDoItemUsers.Add(new ToDoItemUser(Id, userId));
            }
        }
    }
}