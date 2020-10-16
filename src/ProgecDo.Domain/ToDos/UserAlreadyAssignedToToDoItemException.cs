using Volo.Abp;

namespace ProgecDo.ToDos
{
    public class UserAlreadyAssignedToToDoItemException: BusinessException
    {
        public UserAlreadyAssignedToToDoItemException() : base(ProgecDoDomainErrorCodes.UserAlreadyAssignedToToDoItem)
        {
        }
    }
}