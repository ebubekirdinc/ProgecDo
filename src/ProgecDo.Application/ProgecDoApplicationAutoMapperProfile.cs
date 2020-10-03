using AutoMapper;
using ProgecDo.Projects;
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
        }
    }
}