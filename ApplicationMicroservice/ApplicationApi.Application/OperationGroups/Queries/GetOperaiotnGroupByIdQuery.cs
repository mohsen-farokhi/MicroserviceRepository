using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.Queries
{
    public class GetOperaiotnGroupByIdQuery:
        Framework.Mediator.IRequest<OperationGroupViewModel>
    {
        public int? Id { get; set; }
    }
}
