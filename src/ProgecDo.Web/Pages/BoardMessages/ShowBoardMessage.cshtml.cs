using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using ProgecDo.BoardMessages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.BoardMessages
{
    public class ShowBoardMessage : ProgecDoPageModel
    {
        [BindProperty] public NewBoardMessageCommentViewModel NewBoardMessageComment { get; set; }
        public ShowBoardMessageViewModel BoardMessage { get; set; }
        private readonly IBoardMessageAppService _boardMessageAppService;

        public ShowBoardMessage(IBoardMessageAppService boardMessageAppService)
        {
            _boardMessageAppService = boardMessageAppService;
        }

        public async Task OnGetAsync(Guid boardMessageId)
        {
            NewBoardMessageComment = new NewBoardMessageCommentViewModel
            {
                ParentId = boardMessageId
            };
            BoardMessage = ObjectMapper.Map<BoardMessageDto, ShowBoardMessageViewModel>(await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessageId));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _boardMessageAppService.AddCommentToBoardMessage(ObjectMapper.Map<NewBoardMessageCommentViewModel, CreateUpdateBoardMessageCommentDto>(NewBoardMessageComment));

            return new RedirectToPageResult("/BoardMessages/ShowBoardMessage", new {parentId = NewBoardMessageComment.ParentId});
        }

        public class NewBoardMessageCommentViewModel
        {
            [DisplayName("Comment")]
            [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
            [TextArea]
            [Required]
            public string Content { get; set; }

            // [HiddenInput] 
            public Guid ParentId { get; set; }
        }

        public class ShowBoardMessageViewModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public string UserName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime CreationTime { get; set; }

            public List<CommentViewModel> Comments { get; set; }

            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public int CommentCount { get; set; }
        }

        public class CommentViewModel //: AuditedEntityDto<Guid>
        {
            public Guid Id { get; set; }

            public Guid ParentId { get; set; }
            public string Content { get; set; }

            public string UserName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime CreationTime { get; set; }
        }
    }
}