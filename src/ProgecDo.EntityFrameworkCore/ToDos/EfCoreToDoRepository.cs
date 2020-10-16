using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProgecDo.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProgecDo.ToDos
{
    public class EfCoreToDoRepository : EfCoreRepository<ProgecDoDbContext, ToDo, Guid>, IToDoRepository
    {
        public EfCoreToDoRepository(IDbContextProvider<ProgecDoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<ToDoItem> GetToDoItemWithUsersAsync(Guid toDoItemId)
        {
            return await DbContext.ToDoItems
                .Include(x => x.ToDoItemUsers)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == toDoItemId);
        }
    }
}