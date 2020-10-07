using AutoMapper;
using ProgecDo.BoardMessages;
using ProgecDo.ProjectBoard;
using ProgecDo.Projects;
using ProgecDo.ToDos;
using ProgecDo.Users;

namespace ProgecDo
{
    public class ProgecDoApplicationAutoMapperProfile : Profile
    {
        public ProgecDoApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectUser, ProjectUserDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<CreateUpdateProjectDto, Project>();

            CreateMap<AppUser, UserLookupDto>();

            CreateMap<CreateUpdateBoardMessageDto, BoardMessage>();
            // CreateMap<BoardMessage , BoardMessageDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Comment, EditBoardMessageCommentDto>();
            CreateMap<BoardMessage, BoardMessageDto>();

            CreateMap<ToDo, ToDoDto>();
            CreateMap<CreateUpdateToDoDto, ToDo>();
            
            CreateMap<ToDoItem, ShowToDoItemDto>();
        }
    }
}