using Framework.Persistence;
using Framework.ViewModels;
using System.Threading.Tasks;
using UserApi.Domain.Aggregates.Users;
using UserApi.ViewModels.Users;

namespace UserApi.Persistence.Users
{
    public interface IQueryUserRepository : IQueryRepository<User>
    {
        Task<UserViewModel> GetByIdAsync(int id);

        Task<ViewPagingDataResult<UserViewModel>> GetAllAsync
            (GetAllUserRequestViewModel request);
    }
}
