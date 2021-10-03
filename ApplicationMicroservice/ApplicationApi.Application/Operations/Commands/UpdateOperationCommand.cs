using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.Commands
{
    public class UpdateOperationCommand :
        UpdateOperationViewModel, Framework.Mediator.IRequest
    {
    }
}
