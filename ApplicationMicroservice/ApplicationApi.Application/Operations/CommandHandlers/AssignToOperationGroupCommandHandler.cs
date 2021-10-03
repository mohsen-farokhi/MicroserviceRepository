using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Operations.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Operations.CommandHandlers
{
    public class AssignToOperationGroupCommandHandler :
        Framework.Mediator.IRequestHandler<AssignToOperationGroupCommand>
    {
        public AssignToOperationGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (AssignToOperationGroupCommand request, CancellationToken cancellationToken)
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
                await UnitOfWork.OperationRepository.FindAsync
                (request.OperationId.Value);

            if(foundedOperation == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Operation);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var assignResult = 
                foundedOperation.AssignToOperationGroup(foundedOperationGroup);

            if (assignResult.IsFailed)
            {
                result.WithErrors(errors: assignResult.Errors);

                return result;
            }

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Operation);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
