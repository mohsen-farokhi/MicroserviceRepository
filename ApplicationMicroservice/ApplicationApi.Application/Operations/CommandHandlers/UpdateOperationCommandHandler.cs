using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Operations.CommandHandlers
{
    public class UpdateOperationCommandHandler :
        Framework.Mediator.IRequestHandler<UpdateOperationCommand>
    {
        public UpdateOperationCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            if (request.ApplicationId.HasValue == false ||
                request.AccessType.HasValue == false ||
                request.Id.HasValue == false)
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

            // **************************************************
            var foundedOperation =
                await UnitOfWork.OperationRepository.FindAsync
                (id: request.Id.Value);

            if (foundedOperation == null)
            {
                var errorMessage = string.Format
                   (Resources.Messages.Validations.NotFound,
                   Resources.DataDictionary.Operation);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var operationResult =
                foundedOperation.Update
                (application: foundedApplication, name: request.Name,
                displayName: request.DisplayName, isActive: request.IsActive,
                accessType: request.AccessType.Value);

            if (operationResult.IsFailed)
            {
                result.WithErrors(errors: operationResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessUpdate,
                Resources.DataDictionary.Operation);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
