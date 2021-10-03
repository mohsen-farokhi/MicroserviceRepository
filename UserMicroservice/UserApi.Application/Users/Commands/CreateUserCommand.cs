using UserApi.ViewModels.Users;

namespace UserApi.Application.Users.Commands
{
    public class CreateUserCommand :
        CreateUserViewModel, Framework.Mediator.IRequest<int>
    {
    }
}
