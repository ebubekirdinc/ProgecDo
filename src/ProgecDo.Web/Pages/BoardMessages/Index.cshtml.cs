using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;

namespace ProgecDo.Web.Pages.BoardMessages
{
    public class Index : ProgecDoPageModel
    {
        public BoardMessagesViewModel BoardMessage { get; set; }
        private readonly IBoardMessageAppService _boardMessageAppService;

        public Index(IBoardMessageAppService boardMessageAppService)
        {
            _boardMessageAppService = boardMessageAppService;
        }

        public async Task OnGetAsync(Guid projectId)
        {
            BoardMessage = ObjectMapper.Map<BoardMessageListDto, BoardMessagesViewModel>(await _boardMessageAppService.GetBoardMessageListByProjectId(projectId));
        }

        public class BoardMessagesViewModel
        {
            public Guid ProjectId { get; set; }
            public string ProjectTitle { get; set; }
            public string ProjectDescription { get; set; }
            public List<BoardMessageWithUserViewModel> BoardMessageList { get; set; }
        }
        
        public class BoardMessageWithUserViewModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public int CommentCount { get; set; }
            public DateTime CreationTime { get; set; }
        }
    }
}