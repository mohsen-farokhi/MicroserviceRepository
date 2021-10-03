using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Applications.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Application.Applications.QueryHandlers
{
    public class GetAllApplicationQueryHandler :
        Framework.Mediator.IRequestHandler<GetAllApplicationQuery, IList<ApplicationViewModel>>
    {
        public GetAllApplicationQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public  async Task<Framework.Result<IList<ApplicationViewModel>>> Handle
            (GetAllApplicationQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<IList<ApplicationViewModel>>();

            var applications =
                await UnitOfWork.ApplicationRepository.GetAllAsync(request.IsActive);

            result.WithValue(value: applications);

            return result;
        }
    }
}
