using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ProgecDo.BoardMessages; 
using ProgecDo.Projects; 
using Shouldly;
using Volo.Abp.Application.Dtos; 
using Xunit;

namespace ProgecDo.Pages.BoardMessages
{
    public class BoardMessagesTests : ProgecDoWebTestBase
    {
        private readonly IBoardMessageAppService _boardMessageAppService;
        private readonly IProjectAppService _projectAppService;

        public BoardMessagesTests()
        {
            _boardMessageAppService = GetRequiredService<IBoardMessageAppService>();
            _projectAppService = GetRequiredService<IProjectAppService>();
        }

        [Fact]
        public async Task Should_Get_IndexPage()
        {
            // Arrange
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/BoardMessages?projectId=" + project.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_ShowBoardMessagePage()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/BoardMessages/ShowBoardMessage?parentId=" + boardMessages.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_CreateModal()
        {
            // Arrange
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/BoardMessages/CreateModal?projectId=" + project.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_EditBoardMessageModal()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/BoardMessages/EditBoardMessageModal?parentId=" + boardMessages.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_EditCommentModal()
        {
            // Arrange
            var boardMessages = await _boardMessageAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            var boardMessageDto = await _boardMessageAppService.GetBoardMessageWithCommentsByBoardMessageId(boardMessages.Items.FirstOrDefault().Id);

            // Act 
            var response = await Client.GetAsync("/BoardMessages/EditCommentModal?commentId=" + boardMessageDto.Comments.FirstOrDefault().Id + "&parentId=" + boardMessages.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}