using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.OperationGroups.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.OperationGroups.CommandHandlers
{
    public class CreateOperationGroupCommandHandler :
        Framework.Mediator.IRequestHandler<CreateOperationGroupCommand, int>
    {
        public CreateOperationGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<int>> Handle
            (CreateOperationGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            if (request.ApplicationId.HasValue == false)
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
            var operationGroupResult =
                Domain.Aggregates.OperationGroups.OperationGroup.Create
                (application: foundedApplication,
                name: request.Name,
                isActive: request.IsActive);

            if (operationGroupResult.IsFailed)
            {
                result.WithErrors(errors: operationGroupResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.OperationGroupRepository.AddAsync
                (operationGroupResult.Value);

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.OperationGroup);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
