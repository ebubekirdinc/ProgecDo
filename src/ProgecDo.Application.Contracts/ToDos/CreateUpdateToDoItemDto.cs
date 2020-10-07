using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.ToDos
{
    [Serializable]
    public class CreateUpdateToDoItemDto
    {
        public Guid Id { get; set; }

        // [Required]
        // [StringLength(ToDoConsts.MaxNameLength, MinimumLength = ToDoConsts.MinNameLength)]
        // public string Name { get; set; }

        [StringLength(ToDoConsts.MaxToDoItemDescriptionLength)]
        public string Description { get; set; }
 
        public virtual DateTime? DueDate { get; private set; }
        
        [Required] public Guid ParentId { get; set; }
    }
}