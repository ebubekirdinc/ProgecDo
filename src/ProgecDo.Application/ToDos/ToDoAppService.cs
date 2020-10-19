using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;
using ProgecDo.ProjectBoard;
using ProgecDo.Projects;
using ProgecDo.Users;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ProgecDo.ToDos
{
    public class ToDoAppService : CrudAppService<ToDo, ToDoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateToDoDto>, IToDoAppService
    {
        private readonly ToDoManager _toDoManager;
        private readonly IProjectRepository _projectRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;
        private readonly IRepository<ToDoItem, Guid> _toDoItemRepository;
        private readonly IToDoRepository _toDoRepository;

        public ToDoAppService(IRepository<ToDo, Guid> repository, ToDoManager toDoManager, IProjectRepository projectRepository,
            IRepository<AppUser, Guid> userRepository, IRepository<ToDoItem, Guid> toDoItemRepository, IToDoRepository toDoRepository) : base(repository)
        {
            _toDoManager = toDoManager;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _toDoItemRepository = toDoItemRepository;
            _toDoRepository = toDoRepository;
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

        public async Task<ToDoListDto> GetToDoListByProjectId(Guid projectId)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == projectId);
            var query = await _toDoRepository.GetToDoListsByProjectId(projectId); 

            return new ToDoListDto
            {
                ProjectId = projectId,
                ProjectTitle = project.Title,
                ProjectDescription = project.Description,
                ToDos = ObjectMapper.Map<List<ToDo>, List<ToDoDto>>(query)
            };
        }

        public async Task<ToDoDto> GetToDoListWithToDoItemsByToDoListId(Guid toDoListId)
        {
            var toDoItems = _toDoItemRepository.Where(x => x.ParentId == toDoListId);

            var query = from toDoItem in toDoItems.ToList()
                join user in _userRepository on toDoItem.CreatorId equals user.Id
                select new ToDoItemDto
                {
                    Id = toDoItem.Id,
                    Description = toDoItem.Description,
                    CreationTime = toDoItem.CreationTime,
                    DueDate = toDoItem.DueDate,
                    // UserName = user.UserName,
                    // Name = user.Name,
                    // Surname = user.Surname,
                    ParentId = toDoItem.ParentId
                };


            var toDoDtoList = from toDoList in Repository.Where(x => x.Id == toDoListId)
                join user in _userRepository on toDoList.CreatorId equals user.Id
                join project in _projectRepository on toDoList.ProjectId equals project.Id
                select new ToDoDto
                {
                    Id = toDoListId,
                    Name = toDoList.Name,
                    Description = toDoList.Description,
                    ProjectId = project.Id,
                    ProjectTitle = project.Title,
                    ProjectDescription = project.Description,
                    CreationTime = toDoList.CreationTime,
                    // Name = user.Name,
                    // UserName = user.UserName,
                    // Surname = user.Surname,
                    // CommentCount = _boardMessageCommentRepository.Count(x => x.ParentId == toDoList.Id),
                    ToDoItems = query.ToList()
                };


            return toDoDtoList.FirstOrDefault();
        }

        public async Task<bool> AddToDoItem(CreateUpdateToDoItemDto input)
        {
            var toDo = Repository.WithDetails(x => x.ToDoItems).FirstOrDefault(x => x.Id == input.ParentId);
            toDo?.AddToDoItem(input.Description, input.DueDate);

            await Repository.UpdateAsync(toDo);

            return true;
        }

        public async Task<ToDoItemDto> GetToDoItemById(Guid toDoListId, Guid toDoItemId)
        {
            var toDoList = await Repository.GetAsync(toDoListId);
            // var toDoItem = await _toDoItemRepository.GetAsync(toDoItemId);
            var project = await _projectRepository.GetAsync(x => x.Id == toDoList.ProjectId);

            // var toDoItemDto = ObjectMapper.Map<ToDoItem, ShowToDoItemDto>(toDoItem);
            var toDoItem = await _toDoRepository.GetToDoItemWithUsersAsync(toDoItemId);

            var showToDoItemDto = ObjectMapper.Map<ToDoItem, ToDoItemDto>(toDoItem);


            showToDoItemDto.ProjectId = project.Id;
            showToDoItemDto.ProjectTitle = project.Title;
            showToDoItemDto.ProjectDescription = project.Description;
            showToDoItemDto.ToDoListName = toDoList.Name;

            return showToDoItemDto;
        }

        public async Task<bool> UpdateToDoItemAsync(CreateUpdateToDoItemDto input)
        {
            var toDoItem = await _toDoItemRepository.GetAsync(input.Id);

            await _toDoManager.UpdatToDoItem(toDoItem, input.Description, input.Note, input.DueDate);
            await _toDoItemRepository.UpdateAsync(toDoItem);

            return true;
        }

        public bool DeleteToDoItem(Guid toDoItemId, Guid toDoListId)
        {
            var toDo = Repository
                .WithDetails(x => x.ToDoItems)
                .FirstOrDefault(x => x.Id == toDoListId);

            toDo?.DeleteToDoItem(toDoItemId);

            return true;
        }

        public async Task<bool> AssignUserToToDoItem(Guid toDoItemId, Guid userId)
        {
            var toDoItem = _toDoItemRepository.WithDetails(x => x.ToDoItemUsers)
                .FirstOrDefault(x => x.Id == toDoItemId);
            toDoItem?.AssignUserToToDoItem(userId);

            await _toDoItemRepository.UpdateAsync(toDoItem);

            return true;
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(ObjectMapper.Map<List<AppUser>, List<UserLookupDto>>(users));
        }
    }
}