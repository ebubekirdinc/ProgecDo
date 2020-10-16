using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;
using ProgecDo.Projects;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.ProjectBoard
{
    public class ProjectBoardAppService : ProgecDoAppService, IProjectBoardAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectAppService _projectAppService;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IBoardMessageAppService _boardMessageAppService;

        public ProjectBoardAppService(IProjectRepository projectRepository, IProjectAppService projectAppService, IRepository<AppUser, Guid> userRepository, IBoardMessageAppService boardMessageAppService)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _boardMessageAppService = boardMessageAppService;
            _projectAppService = projectAppService;
        }

        public async Task<ProjectBoardDto> GetProjectBoardAsync(Guid id)
        {
            var query = _projectRepository
                .Where(x => x.Id == id)
                .Select(x => new ProjectBoardDto()
                {
                    ProjectId = x.Id,
                    Title = x.Title,
                    Description = x.Description
                });

            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            queryResult.BoardMessageDto = await _boardMessageAppService.GetBoardMessageListByProjectId(id);
            
            return queryResult;
        }

        public async Task<bool> AssignUserToProject(Guid projectId, Guid userId)
        {
            var project = _projectRepository.WithDetails(x => x.ProjectUsers)
                .FirstOrDefault(x => x.Id == projectId);
            project?.AssignUserToProject(userId);

            await _projectRepository.UpdateAsync(project);

            return true;
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(ObjectMapper.Map<List<AppUser>, List<UserLookupDto>>(users));
        }
    }
}