using System.Threading;
using System.Threading.Tasks;
using ApplicationApi.Application.Applications.Commands;
using ApplicationApi.Persistence;

namespace ApplicationApi.Application.Applications.CommandHandlers
{
    public class CreateApplicationCommandHandler :
        Framework.Mediator.IRequestHandler<CreateApplicationCommand, int>
    {
        public CreateApplicationCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<int>> Handle
            (CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            // **************************************************
            var applicationResut =
                Domain.Aggregates.Applications.Application.Create
                (name: request.Name, 
                displayName: request.DisplayName, 
                isActive: request.IsActive);

            if (applicationResut.IsFailed)
            {
                result.WithErrors(errors: applicationResut.Errors);

                return result;
            }
            // **************************************************

            var application =
                await UnitOfWork.ApplicationRepository.AddAsync
                (entity: applicationResut.Value);

            await UnitOfWork.SaveAsync();

            result.WithValue(value: application.Id);

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Application);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
