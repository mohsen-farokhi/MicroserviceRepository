using System.Collections.Generic;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Application.Applications.Queries
{
    public class GetAllApplicationQuery:
        Framework.Mediator.IRequest<IList<ApplicationViewModel>>
    {
        public bool IsActive { get; set; }
    }
}
