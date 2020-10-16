using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.ToDos
{
    [Serializable]
    public class CreateUpdateToDoItemDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(ToDoConsts.MaxToDoItemDescriptionLength, MinimumLength = ToDoConsts.MinToDoItemDescriptionLength)]
        public string Description { get; set; }

        [StringLength(ToDoConsts.MaxToDoItemNoteLength)]
        public string Note { get; set; }
 
        public virtual DateTime? DueDate { get; private set; }
        
        [Required] public Guid ParentId { get; set; }
    }
}