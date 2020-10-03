using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProgecDo.Projects
{
    public interface IProjectAppService : ICrudAppService< //Defines CRUD methods
        ProjectDto, //Used to show projects
        Guid, //Primary key of the project entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProjectDto> //Used to create/update a project
    {
        public Task<ListResultDto<ProjectDto>> GetProjectsWithUsersAsync();
    }
}