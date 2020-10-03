using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace ProgecDo.Projects
{
    public class ProjectManager : DomainService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateAsync([NotNull] string title, Guid userId, [CanBeNull] string description = null)
        {
            Check.NotNullOrWhiteSpace(title, nameof(title));

            var existingProject = await _projectRepository.FindByTitleAsync(title);
            if (existingProject != null)
            {
                throw new ProjectAlreadyExistsException(title);
            }

            return new Project(
                GuidGenerator.Create(),
                title,
                description,
                userId);
        }
    }
}