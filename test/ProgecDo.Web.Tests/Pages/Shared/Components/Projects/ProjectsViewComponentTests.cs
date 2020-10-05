using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ProgecDo.Projects;
using ProgecDo.Web.Pages.Shared.Components.Projects;
using Shouldly;
using Xunit;

namespace ProgecDo.Pages.Shared.Components.Projects
{
    public class ProjectsViewComponentTests : ProgecDoWebTestBase
    {
        private readonly IProjectAppService _projectAppService;

        public ProjectsViewComponentTests()
        {
            _projectAppService = GetRequiredService<IProjectAppService>();
        }

        [Fact]
        public async Task Should_Get_ViewComponent_With_ProjectsViewModel()
        {
            // Arrange
            var viewComponent = new ProjectsViewComponent(_projectAppService);

            // Act
            var result = await viewComponent.InvokeAsync() as ViewViewComponentResult;
            var model = result.ViewData.Model as ProjectsViewComponent.ProjectsViewModel;

            // Assert 
            result.ViewData.Model.ShouldNotBeNull();
            result.ShouldBeOfType<ViewViewComponentResult>();
            result.ViewData.Model.ShouldBeOfType<ProjectsViewComponent.ProjectsViewModel>();
            model.Projects.Items.Count.ShouldBeGreaterThan(0);
            model.Projects.Items.ShouldContain(x => x.Title == ProjectConsts.FirstProjectTitle);
        }
    }
}