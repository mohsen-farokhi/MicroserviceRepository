using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Users.CommandHandlers
{
    public class CreateUserCommandHandler :
        Framework.Mediator.IRequestHandler<CreateUserCommand, int>
    {
        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<int>> Handle
            (CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            // **************************************************
            var userResult =
                Domain.Aggregates.Users.User.Create
                (username: request.Username,
                password: request.Password,
                emailAddress: request.EmailAddress,
                role: request.Role,
                gender: request.Gender,
                firstName: request.FirstName,
                lastName: request.LastName,
                cellPhoneNumber: request.CellPhoneNumber);

            if (userResult.IsFailed)
            {
                result.WithErrors(errors: userResult.Errors);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedUser =
                await UnitOfWork.UserRepository.GetByUsernameAsync
                (userResult.Value.Username);

            if (foundedUser != null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.Username);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            var user =
                await UnitOfWork.UserRepository.AddAsync
                (userResult.Value);

            await UnitOfWork.SaveAsync();

            result.WithValue(value: user.Id);

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.User);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
