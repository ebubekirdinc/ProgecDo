using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.Projects;
using ProgecDo.Users;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ProgecDo.ProjectBoard
{
    public class ProjectBoardAppServiceTests : ProgecDoApplicationTestBase
    {
        private readonly IProjectBoardAppService _projectBoardAppService;
        private readonly IProjectRepository _projectRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IProjectAppService _projectAppService;

        public ProjectBoardAppServiceTests()
        {
            _projectAppService = GetRequiredService<IProjectAppService>();
            _projectBoardAppService = GetRequiredService<IProjectBoardAppService>();
            _projectRepository = GetRequiredService<IProjectRepository>();
            _userRepository = GetRequiredService<IRepository<AppUser, Guid>>();
        }

        [Fact]
        public async Task Should_Get_ProjectBoard_By_Id()
        {
            // Arrange
            var projects = await _projectRepository.GetListAsync();
            var firstProject = projects.First();

            // Act
            var result = await _projectBoardAppService.GetProjectBoardAsync(firstProject.Id);

            // Assert 
            result.Title.ShouldBe(ProjectConsts.FirstProjectTitle);
        }

        [Fact]
        public async Task Should_Assign_User_To_Project()
        {
            // Arrange
            var createdProject = await _projectAppService.CreateAsync(
                new CreateUpdateProjectDto
                {
                    Title = "My Test Project",
                    Description = "Test Description"
                });
            var users = await _userRepository.GetListAsync();
            var firstUser = users.First();

            // Act
            var result = await _projectBoardAppService.AssignUserToProject(createdProject.Id, firstUser.Id);

            // Assert 
            result.ShouldNotBeNull();
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Not_Allow_To_Assign_Duplicate_User_To_Project()
        {
            // Arrange
            var projects = await _projectRepository.GetListAsync();
            var firstProject = projects.First();
            var users = await _userRepository.GetListAsync();
            var firstUser = users.First();

            // Act
            var exception = await Assert.ThrowsAsync<UserAlreadyAssignedToProjectException>(async () =>
            {
                await _projectBoardAppService.AssignUserToProject(firstProject.Id, firstUser.Id);
            });

            // Assert 
            exception.ShouldBeOfType(typeof(UserAlreadyAssignedToProjectException));
        }
        
        [Fact]
        public async Task Should_Get_User_Lookup()
        {
            // Act
            var result = await _projectBoardAppService.GetUserLookupAsync(); 
 
            // Assert 
            result.Items.Count.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(x=>x.Name=="admin");
        }
    }
}