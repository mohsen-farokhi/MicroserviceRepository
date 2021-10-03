using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Application.Applications.Queries
{
    public class GetApplicationByIdQuery :
        Framework.Mediator.IRequest<ApplicationViewModel>
    {
        public int? Id { get; set; }
    }
}
