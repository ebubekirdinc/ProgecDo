using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProgecDo.ProjectBoard
{
    public interface IProjectBoardAppService : IApplicationService
    {
        Task<ProjectBoardDto> GetProjectBoardAsync(Guid id);
        Task<bool> AssignUserToProject(Guid projectId, Guid userId);
        public Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
    }
}