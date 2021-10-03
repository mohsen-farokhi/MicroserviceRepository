using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Applications.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Application.Applications.QueryHandlers
{
    public class GetApplicationByIdQueryHandler :
        Framework.Mediator.IRequestHandler<GetApplicationByIdQuery, ApplicationViewModel>
    {
        public GetApplicationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<ApplicationViewModel>> Handle
            (GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<ApplicationViewModel>();

            if (request.Id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);
                return result;
            }

            // **************************************************
            var foundedApplication =
                await UnitOfWork.ApplicationRepository.GetByIdAsync(id: request.Id.Value);

            if (foundedApplication == null)
            {
                var errprMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Application);

                result.WithError(errorMessage: errprMessage);

                return result;
            }
            // **************************************************

            result.WithValue(value: foundedApplication);

            return result;
        }
    }
}
