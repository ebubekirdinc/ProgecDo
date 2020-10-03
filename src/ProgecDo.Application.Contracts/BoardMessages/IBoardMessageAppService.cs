using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProgecDo.BoardMessages
{
    public interface IBoardMessageAppService : ICrudAppService<BoardMessageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBoardMessageDto>
    {
        Task<BoardMessageListDto> GetBoardMessageListByProjectId(Guid projectId);
        Task<BoardMessageDto> GetBoardMessageWithCommentsByParentId(Guid parentId);
        Task<bool> AddCommentToBoardMessage(CreateUpdateBoardMessageCommentDto map);
        EditBoardMessageCommentDto GetBoardMessageCommentByCommentId(Guid commentId, Guid parentId);
        bool UpdateBoardMessageCommentAsync(EditBoardMessageCommentDto boardMessageCommentDto);
        bool DeleteBoardmessageComment(Guid commentId, Guid parentId);
    }
}