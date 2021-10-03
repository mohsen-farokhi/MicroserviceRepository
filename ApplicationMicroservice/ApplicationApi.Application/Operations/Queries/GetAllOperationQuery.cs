using System.Collections.Generic;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.Queries
{
    public class GetAllOperationQuery:
        Framework.Mediator.IRequest<IList<OperationViewModel>>
    {
        public int? ApplicationId { get; set; }

        public bool IsActive { get; set; }
    }
}
