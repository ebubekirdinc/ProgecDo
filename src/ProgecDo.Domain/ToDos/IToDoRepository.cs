using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.ToDos
{
    public interface IToDoRepository : IRepository<ToDo, Guid>
    {
        Task<List<ToDo>> GetToDoListsByProjectId(Guid projectId);
        Task<ToDoItem> GetToDoItemWithUsersAsync(Guid toDoItemId);
    }
}