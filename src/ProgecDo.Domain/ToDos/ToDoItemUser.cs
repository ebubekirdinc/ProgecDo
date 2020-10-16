using System;
using ProgecDo.Users;
using Volo.Abp.Domain.Entities;

namespace ProgecDo.ToDos
{
    public class ToDoItemUser : Entity
    {
        public virtual Guid ToDoItemId { get; private set; }
        public virtual Guid UserId { get; private set; }
        public virtual DateTime CreationTime { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ToDoItem ToDoItem { get; set; }

        protected ToDoItemUser()
        {
        }

        internal ToDoItemUser(Guid toDoItemId, Guid userId)
        {
            ToDoItemId = toDoItemId;
            UserId = userId;
            CreationTime = DateTime.Now;
        }

        public override object[] GetKeys()
        {
            return new object[] {ToDoItemId, UserId};
        }
    }
}