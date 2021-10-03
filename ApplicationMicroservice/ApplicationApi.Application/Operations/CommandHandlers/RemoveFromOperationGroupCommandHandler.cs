using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Operations.CommandHandlers
{
    public class RemoveFromOperationGroupCommandHandler :
        Framework.Mediator.IRequestHandler<RemoveFromOperationGroupCommand>
    {
        public RemoveFromOperationGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (RemoveFromOperationGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            if (request.OperationGroupId.HasValue == false ||
               request.OperationId.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            // **************************************************
            var foundedOperationGroup =
                await UnitOfWork.OperationGroupRepository.FindAsync
                (request.OperationGroupId.Value);

            if (foundedOperationGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedOperation =
                await UnitOfWork.OperationRepository.GetInclueOperationGroupAsync
                (request.OperationId.Value);

            if (foundedOperation == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Operation);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var removeResult = 
                foundedOperation.RemoveFromOperationGroup(foundedOperationGroup);

            if (removeResult.IsFailed)
            {
                result.WithErrors(errors: removeResult.Errors);

                return result;
            }

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessDelete,
                Resources.DataDictionary.Operation);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
