using System;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.ToDos
{
    public class ToDoAppService : CrudAppService<ToDo, ToDoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateToDoDto>, IToDoAppService
    {
        private readonly ToDoManager _toDoManager;
        
        public ToDoAppService(IRepository<ToDo, Guid> repository, ToDoManager toDoManager) : base(repository)
        {
            _toDoManager = toDoManager;
        }

        public override async Task<ToDoDto> CreateAsync(CreateUpdateToDoDto input)
        {
            var toDo = await _toDoManager.CreateAsync(
                input.Name,
                input.Description,
                input.ProjectId);

            await Repository.InsertAsync(toDo);

            return ObjectMapper.Map<ToDo, ToDoDto>(toDo);
        }
    }
}