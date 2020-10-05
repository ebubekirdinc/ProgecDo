using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using ProgecDo.ProjectBoard;
using ProgecDo.Projects;
using ProgecDo.Users;
using ProgecDo.Web.Pages.ProjectBoard;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json;
using Xunit;

namespace ProgecDo.Pages.ProjectBoard
{
    public class ProjectBoardTests : ProgecDoWebTestBase
    {
        private readonly IProjectBoardAppService _projectBoardAppService;
        private readonly IProjectAppService _projectAppService;
        private readonly IRepository<AppUser, Guid> _userRepository;

        public ProjectBoardTests()
        {
            _projectBoardAppService = GetRequiredService<IProjectBoardAppService>();
            _projectAppService = GetRequiredService<IProjectAppService>();
            _userRepository = GetRequiredService<IRepository<AppUser, Guid>>();
        }

        [Fact]
        public async Task Should_Get_IndexPage()
        {
            // Arrange
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/ProjectBoard?projectId=" + project.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_AssignUserToProjectModal()
        {
            // Arrange
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var response = await Client.GetAsync("/ProjectBoard/AssignUserToProjectModal?projectId=" + project.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Throw_UserAlreadyAssignedToProjectException_When_Assigning_An_Already_Assigned_User()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);
            var user = _userRepository.GetListAsync().Result.FirstOrDefault();
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var pageModel = new AssignUserToProjectModal(_projectBoardAppService)
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                AssignUserViewModel = new AssignUserToProjectModal.AssignUserToProjectViewModel
                {
                    ProjectId = project.Items.FirstOrDefault().Id,
                    UserId = user.Id
                }
            };
            var exception = await Assert.ThrowsAsync<UserAlreadyAssignedToProjectException>(async () =>
            {
                var result = await pageModel.OnPostAsync();
            });
        }

        [Fact]
        public async Task Should_Assign_User_To_Project()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext); 
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Act 
            var pageModel = new AssignUserToProjectModal(_projectBoardAppService)
            {
                PageContext = pageContext,
                Url = new UrlHelper(actionContext),
                AssignUserViewModel = new AssignUserToProjectModal.AssignUserToProjectViewModel
                {
                    ProjectId = project.Items.FirstOrDefault().Id,
                    UserId = new Guid()
                }
            };

            var result = await pageModel.OnPostAsync() as NoContentResult;
            result.ShouldNotBeNull();
        }
    }
}