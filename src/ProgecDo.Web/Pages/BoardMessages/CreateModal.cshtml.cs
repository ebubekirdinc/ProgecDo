using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
using ProgecDo.BoardMessages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProgecDo.Web.Pages.BoardMessages
{
    public class CreateModal : ProgecDoPageModel
    {
        [BindProperty] 
        public NewBoardMessageViewModel NewBoardMessage { get; set; }
        private readonly IBoardMessageAppService _boardMessageAppService;

        public CreateModal(IBoardMessageAppService boardMessageAppService)
        {
            _boardMessageAppService = boardMessageAppService;
        }

        public void OnGet(Guid projectId)
        {
            NewBoardMessage = new NewBoardMessageViewModel
            {
                ProjectId = projectId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _boardMessageAppService.CreateAsync(ObjectMapper.Map<NewBoardMessageViewModel, CreateUpdateBoardMessageDto>(NewBoardMessage));
            
            return NoContent();
        }

        public class NewBoardMessageViewModel
        {
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