using UserApi.ViewModels.Users;

namespace UserApi.Application.Users.Commands
{
    public class UpdateUserCommand :
        UpdateUserViewModel, Framework.Mediator.IRequest
    {
    }
}
