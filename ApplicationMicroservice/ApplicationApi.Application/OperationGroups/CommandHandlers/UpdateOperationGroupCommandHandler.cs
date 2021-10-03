using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.OperationGroups.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.OperationGroups.CommandHandlers
{
    public class UpdateOperationGroupCommandHandler :
        Framework.Mediator.IRequestHandler<UpdateOperationGroupCommand>
    {
        public UpdateOperationGroupCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (UpdateOperationGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            if (request.Id.HasValue == false ||
                request.ApplicationId.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }

            // **************************************************
            var foundedOperationGroup =
                await UnitOfWork.OperationGroupRepository
                .FindAsync(request.Id.Value);

            if (foundedOperationGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

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

            foundedOperationGroup.Update
                (application: foundedApplication,
                name: request.Name,
                isActive: request.IsActive);

            await UnitOfWork.SaveAsync();

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessUpdate,
                Resources.DataDictionary.Application);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
