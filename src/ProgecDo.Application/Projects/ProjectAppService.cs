using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgecDo.Permissions;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.Projects
{
    public class ProjectAppService :
        CrudAppService<
            Project, //The Project entity
            ProjectDto, //Used to show Projects
            Guid, //Primary key of the Project entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateProjectDto>, //Used to create/update a Project
        IProjectAppService //implement the IProjectAppService
    {
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly ProjectManager _projectManager;
        private readonly IProjectRepository _projectRepository;

        // public ProjectAppService(IRepository<Project, Guid> repository, IRepository<AppUser, Guid> userRepository, ProjectManager projectManager, IProjectRepository projectRepository) : base(repository)
        public ProjectAppService(IRepository<Project, Guid> repository, IRepository<AppUser, Guid> userRepository, ProjectManager projectManager, IProjectRepository projectRepository) : base(repository)
        {
            _userRepository = userRepository;
            _projectManager = projectManager;
            _projectRepository = projectRepository;

            // Base CrudAppService automatically uses these permissions on the CRUD operations. This makes the application service secure, but also makes the HTTP API secure since this service is automatically used as an HTTP API
            GetPolicyName = ProgecDoPermissions.Projects.Default;
            GetListPolicyName = ProgecDoPermissions.Projects.Default;
            CreatePolicyName = ProgecDoPermissions.Projects.Create;
            UpdatePolicyName = ProgecDoPermissions.Projects.Edit;
            DeletePolicyName = ProgecDoPermissions.Projects.Delete;
        }

        public override async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input)
        {
            var project = await _projectManager.CreateAsync(
                input.Title,
                CurrentUser.Id ?? Guid.NewGuid(), // todo dd
                input.Description);

            await Repository.InsertAsync(project);

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        public async Task<ListResultDto<ProjectDto>> GetProjectsWithUsersAsync()
        {
            var projectUsers = await _projectRepository.GetProjectsWithUsersAsync();

            var query = ObjectMapper.Map<List<Project>, List<ProjectDto>>(projectUsers);

            //  var projectDtos = await AsyncExecuter.ToListAsync(query);


            return new ListResultDto<ProjectDto>(query);
        }
    }
}