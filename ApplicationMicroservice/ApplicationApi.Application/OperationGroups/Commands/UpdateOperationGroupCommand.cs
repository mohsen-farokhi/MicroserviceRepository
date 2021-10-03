using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.Commands
{
    public class UpdateOperationGroupCommand :
        UpdateOperationGroupViewModel, Framework.Mediator.IRequest
    {
    }
}
