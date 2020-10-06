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
            Check.NotNullOrWhiteSpace(description, nameof(Description), ToDoConsts.MaxToDoItemDescriptionLength, ToDoConsts.MinToDoItemDescriptionLength);

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
        public virtual DateTime? DueDate { get; private set; }
        
        public virtual ToDo ToDo { get; set; }

        public ToDoItem()
        {
            
        }
        
        internal ToDoItem(Guid parentId, string description, DateTime? dueDate)
        {
            ParentId = parentId;
            Description = Check.NotNullOrWhiteSpace(description, nameof(Description), ToDoConsts.MaxToDoItemDescriptionLength, ToDoConsts.MinToDoItemDescriptionLength);
            DueDate = dueDate;
        }

        public void SetDescription([NotNull] string description)
        {
            Description = Check.NotNullOrWhiteSpace(description, nameof(Description), maxLength: ToDoConsts.MaxToDoItemDescriptionLength, ToDoConsts.MinToDoItemDescriptionLength); 
        }
    }
}