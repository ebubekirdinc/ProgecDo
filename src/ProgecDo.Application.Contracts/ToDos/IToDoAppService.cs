using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProgecDo.ToDos
{
    public interface IToDoAppService : ICrudAppService<ToDoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateToDoDto>
    {
        Task<ToDoListDto> GetToDoListByProjectId(Guid projectId);
        Task<ToDoDto> GetToDoListWithToDoItemsByToDoListId(Guid toDoListId);
        Task<bool> AddToDoItem(CreateUpdateToDoItemDto input);
        Task<ShowToDoItemDto> GetToDoItemById(Guid toDoListId, Guid toDoItemId);
        Task<bool> UpdateToDoItemAsync(CreateUpdateToDoItemDto input);
        bool DeleteToDoItem(Guid toDoItemId, Guid toDoListId);
    }

}