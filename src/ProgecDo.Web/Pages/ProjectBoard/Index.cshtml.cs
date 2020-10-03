using System;
using System.Threading.Tasks; 
using ProgecDo.BoardMessages;
using ProgecDo.ProjectBoard;

namespace ProgecDo.Web.Pages.ProjectBoard
{
    public class Index : ProgecDoPageModel
    {
        private readonly IProjectBoardAppService _projectBoardAppService;
        public ProjectBoardViewModel ProjectBoard { get; set; }

        public Index(IProjectBoardAppService projectBoardAppService)
        {
            _projectBoardAppService = projectBoardAppService;
        }

        public async Task OnGetAsync(Guid projectId)
        {
            var projectBoardDto = await _projectBoardAppService.GetProjectBoardAsync(projectId);

            ProjectBoard = ObjectMapper.Map<ProjectBoardDto, ProjectBoardViewModel>(projectBoardDto);
        }


        public class ProjectBoardViewModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public BoardMessageListDto BoardMessageDto { get; set; }
        }
    }
}