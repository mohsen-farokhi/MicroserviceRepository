using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.OperationGroups.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Application.OperationGroups.QueryHandlers
{
    public class GetAllOperationGroupQueryHandler :
        Framework.Mediator.IRequestHandler<GetAllOperationGroupQuery,
        IList<OperationGroupViewModel>>
    {
        public GetAllOperationGroupQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<IList<OperationGroupViewModel>>> Handle
            (GetAllOperationGroupQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<IList<OperationGroupViewModel>>();

            if (request.ApplicationId.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            // **************************************************
            var foundedApplication =
                await UnitOfWork.ApplicationRepository.FindAsync
                (id: request.ApplicationId.Value);

            if (foundedApplication == null)
            {
                var errorMessage = string.Format
                   (Resources.Messages.Validations.NotFound,
                   Resources.DataDictionary.Application);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var operationGroups =
                await UnitOfWork.OperationGroupRepository.GetAllByApplicationIdAsync
                (application: foundedApplication, isActive: request.IsActive);

            result.WithValue(value: operationGroups);

            return result;
        }
    }
}
