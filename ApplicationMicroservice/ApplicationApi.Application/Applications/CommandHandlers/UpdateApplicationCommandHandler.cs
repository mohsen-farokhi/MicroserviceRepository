using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Applications.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Applications.CommandHandlers
{
    public class UpdateApplicationCommandHandler :
        Framework.Mediator.IRequestHandler<UpdateApplicationCommand>
    {
        public UpdateApplicationCommandHandler
            (IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var foundedApplication =
                await UnitOfWork.ApplicationRepository.FindAsync(request.Id);

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
            var applicationResult =
                foundedApplication.Update
                (name: request.Name,
                displayName: request.DisplayName,
                isActive: request.IsActive);

            if (applicationResult.IsFailed)
            {
                result.WithErrors(errors: applicationResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.ApplicationRepository.UpdateAsync
                (entity: applicationResult.Value);

            await UnitOfWork.SaveAsync();

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Application);

            result.WithSuccess(successMessage: successMessage);

            return result;

        }
    }
}
