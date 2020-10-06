using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProgecDo.ToDos
{
    public interface IToDoAppService : ICrudAppService<ToDoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateToDoDto>
    {
    }

}