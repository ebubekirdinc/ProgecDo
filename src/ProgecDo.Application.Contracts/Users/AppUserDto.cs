using Volo.Abp.Application.Dtos;

namespace ProgecDo.Users
{
    public class AppUserDto : FullAuditedEntityDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}