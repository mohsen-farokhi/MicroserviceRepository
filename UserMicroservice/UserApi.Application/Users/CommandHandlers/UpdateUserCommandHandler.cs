using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Users.CommandHandlers
{
    public class UpdateUserCommandHandler :
        Framework.Mediator.IRequestHandler<UpdateUserCommand>
    {
        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var foundedUser =
                await UnitOfWork.UserRepository.FindAsync
                (request.Id.Value);

            if (foundedUser == null)
            {
                string errorMessage = string.Format
                        (Resources.Messages.Validations.NotFound,
                        Resources.DataDictionary.User);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var modifiedModel = foundedUser.Update
                (username: request.Username,
                password: request.Password,
                emailAddress: request.EmailAddress,
                role: request.Role,
                gender: request.Gender,
                firstName: request.FirstName,
                lastName: request.LastName,
                cellPhoneNumber: request.CellPhoneNumber);

            if (modifiedModel.IsFailed)
            {
                result.WithErrors(errors: modifiedModel.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessUpdate,
                Resources.DataDictionary.User);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
