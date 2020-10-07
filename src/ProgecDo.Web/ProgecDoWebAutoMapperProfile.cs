using AutoMapper;
using ProgecDo.BoardMessages;
using ProgecDo.ProjectBoard;
using ProgecDo.Projects;
using ProgecDo.ToDos;
using ProgecDo.Users;
using ProgecDo.Web.Pages.BoardMessages;
using ProgecDo.Web.Pages.ToDos;
using Volo.Abp.Application.Dtos;
using Index = ProgecDo.Web.Pages.BoardMessages.Index;

namespace ProgecDo.Web
{
    public class ProgecDoWebAutoMapperProfile : Profile
    {
        public ProgecDoWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<Pages.Projects.CreateModalModel.CreateProjectViewModel, CreateUpdateProjectDto>();
            CreateMap<ProjectDto, Pages.Projects.EditModalModel.EditProjectViewModel>();
            CreateMap<Pages.Projects.EditModalModel.EditProjectViewModel, CreateUpdateProjectDto>();

            CreateMap<AppUser, UserLookupDto>();
            CreateMap<ProjectBoardDto, Pages.ProjectBoard.Index.ProjectBoardViewModel>();

            CreateMap<BoardMessageListDto, Index.BoardMessagesViewModel>();
            CreateMap<BoardMessageWithUserDto, Index.BoardMessageWithUserViewModel>();
            CreateMap<PagedResultDto<BoardMessageWithUserDto>, PagedResultDto<Index.BoardMessageWithUserViewModel>>();
            CreateMap<BoardMessage, BoardMessageListDto>();
            CreateMap<CreateModal.NewBoardMessageViewModel, CreateUpdateBoardMessageDto>();
            CreateMap<BoardMessageDto, ShowBoardMessage.ShowBoardMessageViewModel>();
            CreateMap<ShowBoardMessage.NewBoardMessageCommentViewModel, CreateUpdateBoardMessageCommentDto>();
            CreateMap<CommentDto, ShowBoardMessage.CommentViewModel>();
            CreateMap<PagedResultDto<CommentDto>, PagedResultDto<ShowBoardMessage.CommentViewModel>>();
            CreateMap<Comment, EditBoardMessageCommentDto>();
            CreateMap<EditBoardMessageCommentDto, EditCommentModal.EditCommentViewModel>();
            CreateMap<EditCommentModal.EditCommentViewModel, EditBoardMessageCommentDto>();
            CreateMap<BoardMessageDto, EditBoardMessageModal.EditBoardMessageViewModel>();
            CreateMap<EditBoardMessageModal.EditBoardMessageViewModel, CreateUpdateBoardMessageDto>();

            CreateMap<CreateListModal.NewToDoListViewModel, CreateUpdateToDoDto>();
            CreateMap<ToDoListDto, Pages.ToDos.Index.ToDosViewModel>();
            CreateMap<ToDoListWithToDoItemsDto, Pages.ToDos.Index.ToDoListWithToDoItemsViewModel>();
            CreateMap<ToDoDto, ShowToDoList.ShowToDoListViewModel>();
            CreateMap<ToDoItemDto, ShowToDoList.ToDoItemViewModel>();
            CreateMap<ToDoDto, EditToDoListModal.EditToDoListViewModel>();
            CreateMap<EditToDoListModal.EditToDoListViewModel, CreateUpdateToDoDto>();
        }
    }
}