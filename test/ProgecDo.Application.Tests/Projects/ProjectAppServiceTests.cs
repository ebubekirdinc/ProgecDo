using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.Users;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation;
using Xunit;

namespace ProgecDo.Projects
{
    public class ProjectAppServiceTests : ProgecDoApplicationTestBase
    {
        private readonly IProjectAppService _projectAppService;
        private readonly IRepository<AppUser, Guid> _userRepository;

        public ProjectAppServiceTests()
        {
            _userRepository = GetRequiredService<IRepository<AppUser, Guid>>();
            _projectAppService = GetRequiredService<IProjectAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Projects()
        {
            // Act
            var result = await _projectAppService.GetProjectsWithUsersAsync();

            // Assertp
            result.Items.Count.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(x => x.Title == ProjectConsts.FirstProjectTitle);
        }

        [Fact]
        public async Task Should_Create_A_Valid_Project()
        {
            // Act
            var result = await _projectAppService.CreateAsync(
                new CreateUpdateProjectDto
                {
                    Title = "My Test Project",
                    Description = "Test Description"
                });

            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Title.ShouldBe("My Test Project");
        }
 
        [Theory]
        [InlineData("Aa")] 
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Should_Throw_AbpValidationException_When_Creating_A_Project_With_Invalid_Title(string title)
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _projectAppService.CreateAsync(
                    new CreateUpdateProjectDto
                    {
                        Title = title,
                        Description = "Test Description"
                    }
                );
            });

            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(m => m == "Title"));
        }
        
        [Fact]
        public async Task Should_Not_Allow_To_Create_Duplicate_Project()
        {
            await Assert.ThrowsAsync<ProjectAlreadyExistsException>(async () =>
            {
                await _projectAppService.CreateAsync(
                    new CreateUpdateProjectDto
                    {
                        Title =  ProjectConsts.FirstProjectTitle,
                        Description = "Test Description"
                    }
                );
            });
        }
    }
}