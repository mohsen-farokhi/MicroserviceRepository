using System.Collections.Generic;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.Queries
{
    public class GetAllOperationGroupQuery :
        Framework.Mediator.IRequest<IList<OperationGroupViewModel>>
    {
        public int? ApplicationId { get; set; }

        public bool IsActive { get; set; }
    }
}
