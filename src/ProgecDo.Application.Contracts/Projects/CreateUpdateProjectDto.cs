using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.Projects
{
    [Serializable]
    public class CreateUpdateProjectDto
    {
        [Required] 
        [StringLength(ProjectConsts.MaxTitleLength,MinimumLength = ProjectConsts.MinTitleLength)]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}