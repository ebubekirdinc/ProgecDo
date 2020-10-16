using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.ToDos
{
    public interface IToDoRepository : IRepository<ToDo, Guid>
    {
        Task<ToDoItem> GetToDoItemWithUsersAsync(Guid toDoItemId);
    }
}