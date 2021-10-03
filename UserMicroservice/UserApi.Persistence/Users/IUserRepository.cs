using Framework.Persistence;
using System.Threading.Tasks;
using UserApi.Domain.Aggregates.Users;
using UserApi.Domain.Aggregates.Users.ValueObjects;
using UserApi.ViewModels.Users;

namespace UserApi.Persistence.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<UserViewModel> GetByUsernameAsync(Username username);

        Task<UserViewModel> GetByIdAsync(int id);

        Task<User> GetIncludeGroups(int id);
    }
}
