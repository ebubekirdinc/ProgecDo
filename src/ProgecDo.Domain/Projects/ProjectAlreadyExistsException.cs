using Volo.Abp;

namespace ProgecDo.Projects
{
    public class ProjectAlreadyExistsException : BusinessException
    {
        public ProjectAlreadyExistsException(string title) : base(ProgecDoDomainErrorCodes.ProjectAlreadyExists)
        {
            WithData("title", title);
        }
    }
}