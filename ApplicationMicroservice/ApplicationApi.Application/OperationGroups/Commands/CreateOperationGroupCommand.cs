using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.Commands
{
    public class CreateOperationGroupCommand :
        CreateOperationGroupsViewModel, Framework.Mediator.IRequest<int>
    {

    }
}
