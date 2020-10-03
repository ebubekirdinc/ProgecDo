using System;
using System.ComponentModel.DataAnnotations;

namespace ProgecDo.BoardMessages
{
    public class EditBoardMessageCommentDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
        [Required]
        public string Content { get; set; }
    }
}