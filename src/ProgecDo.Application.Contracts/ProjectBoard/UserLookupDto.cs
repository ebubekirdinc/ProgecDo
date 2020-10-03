using System;
using Volo.Abp.Application.Dtos;

namespace ProgecDo.ProjectBoard
{
    public class UserLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}