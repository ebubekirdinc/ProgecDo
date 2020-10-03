using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        Task<Project> FindByTitleAsync(string name);
        Task<List<Project>> GetProjectsWithUsersAsync();
    }
}