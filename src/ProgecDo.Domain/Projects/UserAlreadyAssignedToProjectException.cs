using Volo.Abp;

namespace ProgecDo.Projects
{
    public class UserAlreadyAssignedToProjectException : BusinessException
    {
        public UserAlreadyAssignedToProjectException() : base(ProgecDoDomainErrorCodes.UserAlreadyAssignedToProject)
        {
        }
    }
}