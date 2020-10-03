using AutoMapper;
using ProgecDo.Projects;

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
        }
    }
}
