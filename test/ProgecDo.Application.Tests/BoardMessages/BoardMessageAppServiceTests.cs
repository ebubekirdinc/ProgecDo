using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.Projects;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace ProgecDo.BoardMessages
{
    public class BoardMessageAppServiceTests : ProgecDoApplicationTestBase
    {
        private readonly IBoardMessageAppService _boardMessageAppService;
        private readonly IProjectRepository _projectRepository;

        public BoardMessageAppServiceTests()
        {
            _boardMessageAppService = GetRequiredService<IBoardMessageAppService>();
            _projectRepository = GetRequiredService<IProjectRepository>();
        }

        [Fact]
        public async Task Should_Create_A_Valid_BoardMessage()
        {
            // Act
            var result = await _boardMessageAppService.CreateAsync(new CreateUpdateBoardMessageDto
            {
                Title = BoardMessageConsts.FirstBoardMessageTitle,
                Content = "test content",
                ProjectId = Guid.NewGuid()
            });

            // Assert 
            result.Title.ShouldBe(BoardMessageConsts.FirstBoardMessageTitle);
        }


        [Theory]
        [InlineData("Aa", "Bb")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public async Task Should_Throw_AbpValidationException_When_Creating_A_Project_With_Invalid_Data(string title, string content)
        {
            // Act
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _boardMessageAppService.CreateAsync(
                    new CreateUpdateBoardMessageDto()
                    {
                        Title = title,
                        Content = content,
                        ProjectId = Guid.NewGuid()
                    }
                );
            });

            // exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(m => m == "Title"));
        }

        [Fact]
        public async Task Should_GetBoardMessageListByProjectId()
        {
            // Arrange
            var project = await _projectRepository.GetListAsync();

            // Act
            var result = await _boardMessageAppService.GetBoardMessageListByProjectId(project.FirstOrDefault().Id);

            // Assert 
            result.ShouldNotBeNull();
            result.BoardMessageList.FirstOrDefault().Title.ShouldBe(BoardMessageConsts.FirstBoardMessageTitle);
        }

        [Fact]
        public async Task Should_GetBoardMessageWithCommentsByParentId()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act
            var result = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Assert 
            result.ShouldNotBeNull();
            result.Title.ShouldBe(BoardMessageConsts.FirstBoardMessageTitle);
            result.Comments.FirstOrDefault().Content.ShouldBe(BoardMessageConsts.FirstBoardMessageCommentContent);
        }

        [Fact]
        public async Task Should_AddCommentToBoardMessage()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act
            var result = await _boardMessageAppService.AddCommentToBoardMessage(
                new CreateUpdateBoardMessageCommentDto
                {
                    Content = BoardMessageConsts.FirstBoardMessageTitle,
                    ParentId = boardMessages.Items.FirstOrDefault().Id
                });

            // Assert 
            result.ShouldBe(true);
        }

        [Theory]
        [InlineData("Aa")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Throw_AbpValidationException_When_Adding_Comment_To_BoardMessage_With_Invalid_Data(string content)
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _boardMessageAppService.AddCommentToBoardMessage(
                    new CreateUpdateBoardMessageCommentDto
                    {
                        Content = content,
                        ParentId = boardMessages.Items.FirstOrDefault().Id
                    }
                );
            });

            // exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(m => m == "Title"));
        }

        [Fact]
        public async Task Should_GetBoardMessageCommentByCommentId()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var boardMessageDto = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Act
            var result = _boardMessageAppService
                .GetBoardMessageCommentByCommentId(boardMessageDto.Comments.FirstOrDefault().Id, boardMessages.Items.FirstOrDefault().Id);

            // Assert 
            result.ShouldNotBeNull();
            result.Content.ShouldBe(BoardMessageConsts.FirstBoardMessageCommentContent);
        }

        [Fact]
        public async Task Should_UpdateBoardMessageComment()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var boardMessageDto = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Act
            var result = _boardMessageAppService.UpdateBoardMessageCommentAsync(
                new EditBoardMessageCommentDto
                {
                    Content = BoardMessageConsts.FirstBoardMessageTitle,
                    ParentId = boardMessages.Items.FirstOrDefault().Id,
                    Id = boardMessageDto.Comments.FirstOrDefault().Id
                });

            // Assert 
            result.ShouldBe(true);
        }

        [Theory]
        [InlineData("Aa")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Throw_AbpValidationException_When_Updating_BoardMessageComment_With_Invalid_Data(string content)
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var boardMessageDto = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Act
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                _boardMessageAppService.UpdateBoardMessageCommentAsync(
                    new EditBoardMessageCommentDto
                    {
                        Content = content,
                        ParentId = boardMessages.Items.FirstOrDefault().Id,
                        Id = boardMessageDto.Comments.FirstOrDefault().Id
                    });
            });

            // exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(m => m == "Title"));
        }

        [Fact]
        public async Task Should_Delete_BoardmessageComment()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var boardMessageDto = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Act
            var result = _boardMessageAppService.DeleteBoardmessageComment(
                boardMessageDto.Comments.FirstOrDefault().Id,
                boardMessages.Items.FirstOrDefault().Id
            );
            var boardMessageDtoAfterDeletion = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Assert 
            result.ShouldBe(true);
            boardMessageDtoAfterDeletion.Comments
                .FirstOrDefault(x => x.Id == boardMessageDto.Comments.FirstOrDefault().Id)
                .ShouldBeNull();
        }
    }
}