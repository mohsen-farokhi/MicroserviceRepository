using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.Queries
{
    public class GetOperaiotnByIdQuery:
        Framework.Mediator.IRequest<OperationViewModel>
    {
        public int? Id { get; set; }
    }
}
