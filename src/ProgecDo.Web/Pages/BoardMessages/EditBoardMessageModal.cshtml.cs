using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgecDo.BoardMessages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.BoardMessages
{
    public class EditBoardMessageModal : ProgecDoPageModel
    {
        [BindProperty] public EditBoardMessageViewModel BoardMessage { get; set; }
        private readonly IBoardMessageAppService _boardMessageAppService;

        public EditBoardMessageModal(IBoardMessageAppService boardMessageAppService)
        {
            _boardMessageAppService = boardMessageAppService;
        }

        public async Task OnGetAsync(Guid boardMessageId)
        {
            var boardMessageDto = await _boardMessageAppService.GetAsync(boardMessageId);

            BoardMessage = ObjectMapper.Map<BoardMessageDto, EditBoardMessageViewModel>(boardMessageDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createUpdateBoardMessageDto = ObjectMapper.Map<EditBoardMessageViewModel, CreateUpdateBoardMessageDto>(BoardMessage);
            
            await _boardMessageAppService.UpdateAsync(BoardMessage.Id, createUpdateBoardMessageDto);

            return NoContent();
        }

        public class EditBoardMessageViewModel
        {
            [HiddenInput] public Guid Id { get; set; }

            [Required]
            [StringLength(BoardMessageConsts.MaxTitleLength)]
            [DisplayName("TitleOfTheBoardMessage")]
            public string Title { get; set; }

            [DisplayName("ContentOfTheBoardMessage")]
            [StringLength(BoardMessageConsts.MaxContentLength, MinimumLength = BoardMessageConsts.MinContentLength)]
            [TextArea]
            public string Content { get; set; }
            
            [HiddenInput] 
            public Guid ProjectId { get; set; } 
        }
    }
}