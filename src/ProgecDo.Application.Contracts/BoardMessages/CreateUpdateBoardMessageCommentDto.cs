using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.BoardMessages
{
    [Serializable]
    public class CreateUpdateBoardMessageCommentDto
    {
        [Required]
        [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
        public string Content { get; set; }

        [Required] public Guid ParentId { get; set; }
    }
}