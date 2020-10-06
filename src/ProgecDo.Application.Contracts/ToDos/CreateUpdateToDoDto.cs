using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.ToDos
{
    [Serializable]
    public class CreateUpdateToDoDto
    {
        [StringLength(ToDoConsts.MaxNameLength, MinimumLength = ToDoConsts.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(ToDoConsts.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required] public Guid ProjectId { get; set; }
    }
}