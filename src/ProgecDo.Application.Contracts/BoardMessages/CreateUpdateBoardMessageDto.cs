using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.BoardMessages
{
    [Serializable]
    public class CreateUpdateBoardMessageDto
    {
        [StringLength(BoardMessageConsts.MaxTitleLength, MinimumLength = BoardMessageConsts.MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
        public string Content { get; set; }

        [Required] 
        public Guid ProjectId { get; set; } 
    }
}