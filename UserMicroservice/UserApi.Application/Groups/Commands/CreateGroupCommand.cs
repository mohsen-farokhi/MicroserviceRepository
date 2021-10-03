using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.Commands
{
    public class CreateGroupCommand :
        CreateGroupViewModel, Framework.Mediator.IRequest<int>
    {
    }
}
