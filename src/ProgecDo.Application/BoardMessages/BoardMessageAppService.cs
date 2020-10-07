using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.Projects;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.BoardMessages
{
    public class BoardMessageAppService : CrudAppService<BoardMessage, BoardMessageDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBoardMessageDto>, IBoardMessageAppService
    {
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IRepository<Comment, Guid> _boardMessageCommentRepository;
        private readonly BoardMessageManager _boardMessageManager;
        private readonly IProjectRepository _projectRepository;

        public BoardMessageAppService(IRepository<BoardMessage, Guid> repository, IRepository<AppUser, Guid> userRepository, IProjectRepository projectRepository, BoardMessageManager boardMessageManager,
            IRepository<Comment, Guid> boardMessageCommentRepository) : base(repository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _boardMessageManager = boardMessageManager;
            _boardMessageCommentRepository = boardMessageCommentRepository;
        }

        public override async Task<BoardMessageDto> CreateAsync(CreateUpdateBoardMessageDto input)
        {
            var boardMessage = await _boardMessageManager.CreateAsync(
                input.Title,
                input.Content,
                input.ProjectId);

            await Repository.InsertAsync(boardMessage);

            return ObjectMapper.Map<BoardMessage, BoardMessageDto>(boardMessage);
        }

        public async Task<BoardMessageListDto> GetBoardMessageListByProjectId(Guid projectId)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == projectId);

            var query = from message in Repository.Where(x => x.ProjectId == projectId)
                join user in _userRepository on message.CreatorId equals user.Id
                select new BoardMessageWithUserDto
                {
                    Id = message.Id,
                    Title = message.Title,
                    Content = message.Content,
                    CreationTime = message.CreationTime,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    CommentCount = _boardMessageCommentRepository.Count(x => x.ParentId == message.Id)
                };

            var queryResult = await AsyncExecuter.ToListAsync(query);

            return new BoardMessageListDto
            {
                ProjectId = projectId,
                ProjectTitle = project.Title,
                ProjectDescription = project.Description,
                BoardMessageList = queryResult.OrderByDescending(x => x.CreationTime).ToList()
            };
        }

        public async Task<BoardMessageDto> GetBoardMessageWithCommentsByBoardMessageId(Guid boardMessageId)
        {
            var comments = _boardMessageCommentRepository.Where(x => x.ParentId == boardMessageId);

            var query = from comment in comments.ToList()
                join user in _userRepository on comment.CreatorId equals user.Id
                select new CommentDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreationTime = comment.CreationTime,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    ParentId = comment.ParentId
                };


            var boardMessage = from message in Repository.Where(x => x.Id == boardMessageId)
                join user in _userRepository on message.CreatorId equals user.Id
                join project in _projectRepository on message.ProjectId equals project.Id
                select new BoardMessageDto
                {
                    Id = boardMessageId,
                    Title = message.Title,
                    Content = message.Content,
                    ProjectId = project.Id,
                    ProjectTitle = project.Title,
                    ProjectDescription = project.Description,
                    CreationTime = message.CreationTime,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    CommentCount = _boardMessageCommentRepository.Count(x => x.ParentId == message.Id),
                    Comments = query.ToList()
                };


            return boardMessage.FirstOrDefault();
        }

        public async Task<bool> AddCommentToBoardMessage(CreateUpdateBoardMessageCommentDto input)
        {
            var boardMessage = Repository.WithDetails(x => x.Comments).FirstOrDefault(x => x.Id == input.ParentId);
            boardMessage?.AddComment(input.Content);

            await Repository.UpdateAsync(boardMessage);

            return true;
        }

        public EditBoardMessageCommentDto GetBoardMessageCommentByCommentId(Guid commentId, Guid parentId)
        {
            // var safdgs = Repository.WithDetails(x=>x.Comments).FirstOrDefault(x => x.Id == boardMessageId);
            // var com=safdgs.Comments.FirstOrDefault(x => x.Id == commentId);

            var comment = Repository
                .WithDetails(x => x.Comments)
                .FirstOrDefault(x => x.Id == parentId)?
                .Comments
                .FirstOrDefault(x => x.Id == commentId);

            return ObjectMapper.Map<Comment, EditBoardMessageCommentDto>(comment);
        }

        public bool UpdateBoardMessageCommentAsync(EditBoardMessageCommentDto boardMessageCommentDto)
        {
            var comment = Repository
                .WithDetails(x => x.Comments)
                .FirstOrDefault(x => x.Id == boardMessageCommentDto.ParentId)?
                .Comments
                .FirstOrDefault(x => x.Id == boardMessageCommentDto.Id);

            comment?.SetContent(boardMessageCommentDto.Content);
            // var sfd = _boardMessageManager.UpdateBoardMessageCommentAsync(comment);
            return true;
        }

        public bool DeleteBoardmessageComment(Guid commentId, Guid parentId)
        {
            var boardMessage = Repository
                .WithDetails(x => x.Comments)
                .FirstOrDefault(x => x.Id == parentId);
            // .Comments
            // .FirstOrDefault(x => x.Id == commentId);

            boardMessage?.DeleteComment(commentId);
            
            return true;
        }
    }
}