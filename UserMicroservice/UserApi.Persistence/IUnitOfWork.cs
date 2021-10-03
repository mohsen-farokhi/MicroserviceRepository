using UserApi.Persistence.Groups;
using UserApi.Persistence.Users;

namespace UserApi.Persistence
{
    public interface IUnitOfWork :
        Framework.Persistence.IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IGroupRepository GroupRepository { get; }
    }
}
