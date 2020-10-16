using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgecDo.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProgecDo.Projects
{
    public class EfCoreProjectRepository : EfCoreRepository<ProgecDoDbContext, Project, Guid>, IProjectRepository
    {
        public EfCoreProjectRepository(IDbContextProvider<ProgecDoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public async Task<Project> FindByTitleAsync(string title)
        {
            return await DbSet.FirstOrDefaultAsync(project => project.Title == title);
        }

        public async Task<List<Project>> GetProjectsWithUsersAsync()
        {
            return await DbContext.Projects
                .Include(x => x.ProjectUsers)
                .ThenInclude(x => x.User).ToListAsync();
        }
    }
}