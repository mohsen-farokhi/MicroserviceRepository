using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.Commands
{
    public class UpdateGroupCommand :
        UpdateGroupViewModel, Framework.Mediator.IRequest
    {
    }
}
