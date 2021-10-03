using UserApi.ViewModels.Users;

namespace UserApi.Application.Users.Queries
{
    public class GetAllUsersQuery :
        GetAllUserRequestViewModel, Framework.Mediator.IRequest
        <Framework.ViewModels.ViewPagingDataResult<UserViewModel>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
