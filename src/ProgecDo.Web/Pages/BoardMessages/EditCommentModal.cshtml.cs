using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgecDo.BoardMessages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.BoardMessages
{
    public class EditCommentModal : ProgecDoPageModel
    {
        [BindProperty] public EditCommentViewModel Comment { get; set; }

        private readonly IBoardMessageAppService _boardMessageAppService;

        public EditCommentModal(IBoardMessageAppService boardMessageAppService)
        {
            _boardMessageAppService = boardMessageAppService;
        }

        public void OnGet(Guid commentId, Guid parentId)
        {
            var commentDto = _boardMessageAppService.GetBoardMessageCommentByCommentId(commentId, parentId);
            Comment = ObjectMapper.Map<EditBoardMessageCommentDto, EditCommentViewModel>(commentDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _boardMessageAppService.UpdateBoardMessageCommentAsync(ObjectMapper.Map<EditCommentViewModel, EditBoardMessageCommentDto>(Comment));

            return NoContent();
        }
        
        public class EditCommentViewModel
        {
            [HiddenInput] public Guid Id { get; set; }
            [HiddenInput] 
            public Guid ParentId { get; set; }

            [DisplayName("Comment")]
            [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
            [TextArea]
            [Required]
            public string Content { get; set; }
        }
    }
}