using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace ProgecDo.BoardMessages
{
    public class BoardMessageManager : DomainService
    {
        // private readonly IRepository<BoardMessage,Guid> _boardMessageRepository;
        // private readonly IBoardMessageRepository _boardMessageRepository;

        public BoardMessageManager() // IRepository<BoardMessage,Guid> boardMessageRepository)
        {
            // _boardMessageRepository = boardMessageRepository;
        }

        public async Task<BoardMessage> CreateAsync([NotNull] string title, [NotNull] string content, Guid projectId)
        {
            Check.NotNullOrWhiteSpace(title, nameof(title));
            Check.NotNullOrWhiteSpace(content, nameof(content));

            return new BoardMessage(
                GuidGenerator.Create(),
                title,
                content,
                projectId);
        }

        // public async Task UpdateBoardMessageCommentAsync([NotNull] BoardMessageComment boardMessageComment)
        // {
        //     Check.NotNull(boardMessageComment, nameof(boardMessageComment));
        //     Check.NotNullOrWhiteSpace(boardMessageComment.Content, nameof(boardMessageComment.Content));
        //
        //     var boardMessage = await _boardMessageRepository.GetAsync(x => x.Id == boardMessageComment.BoardMessageId);
        //
        //     var comment = boardMessage.BoardMessageComments.FirstOrDefault(x => x.Id == boardMessageComment.Id);
        //     comment.SetContent(comment.Content);
        // }
    }
}