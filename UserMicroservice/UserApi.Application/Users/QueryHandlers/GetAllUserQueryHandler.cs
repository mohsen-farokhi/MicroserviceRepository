using Framework.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Queries;
using UserApi.ViewModels.Users;

namespace UserApi.Application.Users.QueryHandlers
{
    public class GetAllUserQueryHandler :
        Framework.Mediator.IRequestHandler
        <GetAllUsersQuery, ViewPagingDataResult<UserViewModel>>
    {
        public GetAllUserQueryHandler
            (Persistence.IQueryUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected Persistence.IQueryUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<ViewPagingDataResult<UserViewModel>>> Handle
            (GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<ViewPagingDataResult<UserViewModel>>();

            var users =
                await UnitOfWork.UserRepository
                .GetAllAsync(request);

            result.WithValue(value: users);

            return result;
        }
    }
}
