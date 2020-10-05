using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using ProgecDo.Projects;
using ProgecDo.Web.Pages;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json;
using Xunit;

namespace ProgecDo.Pages.Projects
{ 
    public class FakeUserClaims
    {
        public List<Claim> Claims { get; } = new List<Claim>();
    }

    public class ProjectsTests : ProgecDoWebTestBase
    {
        private readonly IProjectAppService _projectAppService; 


        public ProjectsTests()
        {
            _projectAppService = GetRequiredService<IProjectAppService>();
        }

        [Fact]
        public async Task Should_Get_CreateModal()
        {
            // Act 
            var response = await Client.GetAsync("/Projects/CreateModal");

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_EditModal()
        {
            // Arrange
            var project = await _projectAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            
            // Act 
            var response = await Client.GetAsync("/Projects/EditModal?id=" + project.Items.FirstOrDefault()?.Id);

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_IndexPage()
        {
            // Act 
            var response = await Client.GetAsync("/");

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task Should_Get_ProjectsViewComponent()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext); 
                
            // Act 
            var pageModel = new IndexModel()
            {
                PageContext = pageContext, 
                Url = new UrlHelper(actionContext)
            };
            var result =  pageModel.OnGetProjectsViewComponent() as ViewComponentResult;
                
            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<ViewComponentResult>(); 
        }
   
 
    }
}