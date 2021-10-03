using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Queries;
using ApplicationApi.Persistence;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Application.Operations.QueryHandlers
{
    public class GetAllOperationQueryHandler :
        Framework.Mediator.IRequestHandler<GetAllOperationQuery, IList<OperationViewModel>>
    {
        public GetAllOperationQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<IList<OperationViewModel>>> Handle
            (GetAllOperationQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<IList<OperationViewModel>>();

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

            var operations =
                await UnitOfWork.OperationRepository.GetAllAsync
                (application: foundedApplication,
                isActive: request.IsActive);

            result.WithValue(value: operations);

            return result;
        }
    }
}
