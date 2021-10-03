using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Operations.CommandHandlers
{
    public class CreateOperationCommandHandler :
        Framework.Mediator.IRequestHandler<CreateOperationCommand, int>
    {
        public CreateOperationCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<int>> Handle
            (CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            if (request.ApplicationId.HasValue == false ||
                request.AccessType.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            // **************************************************
            var foundedApplication =
                await UnitOfWork.ApplicationRepository.FindAsync
                (request.ApplicationId.Value);

            if (foundedApplication == null)
            {
                var errorMessage = string.Format
                   (Resources.Messages.Validations.NotFound,
                   Resources.DataDictionary.Application);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var operationResult =
                Domain.Aggregates.Operations.Operation.Create
                (application: foundedApplication,name: request.Name,
                displayName: request.DisplayName, isActive: request.IsActive,
                accessType: request.AccessType.Value);

            if (operationResult.IsFailed)
            {
                result.WithErrors(errors: operationResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.OperationRepository.AddAsync
                (operationResult.Value);

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Operation);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
