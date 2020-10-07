using System;
using System.Linq;
using System.Threading.Tasks;
using ProgecDo.BoardMessages;
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

        public ToDoAppService(IRepository<ToDo, Guid> repository, ToDoManager toDoManager, IProjectRepository projectRepository,
            IRepository<AppUser, Guid> userRepository, IRepository<ToDoItem, Guid> toDoItemRepository) : base(repository)
        {
            _toDoManager = toDoManager;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _toDoItemRepository = toDoItemRepository;
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

            var query = from toDo in Repository.Where(x => x.ProjectId == projectId)
                join user in _userRepository on toDo.CreatorId equals user.Id
                select new ToDoListWithToDoItemsDto
                {
                    Id = toDo.Id,
                    Name = toDo.Name,
                    Description = toDo.Description,
                    CreationTime = toDo.CreationTime,
                    ToDoListCreatorUserName = user.UserName,
                    ToDoListCreatorName = user.Name,
                    ToDoListCreatorSurname = user.Surname,
                    // CommentCount = _boardMessageCommentRepository.Count(x => x.ParentId == toDo.Id)
                };

            var queryResult = await AsyncExecuter.ToListAsync(query);

            return new ToDoListDto
            {
                ProjectId = projectId,
                ProjectTitle = project.Title,
                ProjectDescription = project.Description,
                ToDoListWithToDoItems = queryResult.OrderByDescending(x => x.CreationTime).ToList()
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


            return  toDoDtoList.FirstOrDefault();
        }
        
        public async Task<bool> AddToDoItem(CreateUpdateToDoItemDto input)
        {
            var toDo = Repository.WithDetails(x => x.ToDoItems).FirstOrDefault(x => x.Id == input.ParentId);
            toDo?.AddToDoItem(input.Description,input.DueDate);

            await Repository.UpdateAsync(toDo);

            return true;
        }
        
        
        
        
        
        
        
        
        
        
        
    }
}