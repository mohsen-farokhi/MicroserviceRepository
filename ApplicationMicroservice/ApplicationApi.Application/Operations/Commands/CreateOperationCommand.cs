using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.Commands
{
    public class CreateOperationCommand :
        CreateOperationViewModel, Framework.Mediator.IRequest<int>
    {
    }
}
