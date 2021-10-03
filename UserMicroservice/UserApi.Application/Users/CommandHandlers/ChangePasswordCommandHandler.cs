using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Users.CommandHandlers
{
    public class ChangePasswordCommandHandler :
        Framework.Mediator.IRequestHandler<ChangePasswordCommand>
    {
        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            // **************************************************
            if (request.Id is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.Id);

                result.WithError(errorMessage: errorMessage);
                return result;
            }
            // **************************************************

            // **************************************************
            var oldPasswordResult =
                Domain.Aggregates.Users.ValueObjects.Password.Create(request.OldPassword);

            if (oldPasswordResult.IsFailed)
            {
                result.WithErrors(errors: oldPasswordResult.Errors);
                return result;
            }
            // **************************************************

            // **************************************************
            var foundedUser =
                (await UnitOfWork.UserRepository.Find(current =>
                current.Id == request.Id &&
                current.Password == oldPasswordResult.Value)).FirstOrDefault();

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
            var userResult = foundedUser.ChangePassword
                (newPassword: request.NewPassword,
                confirmNewPassword: request.ConfirmNewPassword);

            if (userResult.IsFailed)
            {
                result.WithErrors(errors: userResult.Errors);
                return result;
            }
            // **************************************************

            await UnitOfWork.SaveAsync();

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessUserPasswordChanged);

            result.WithSuccess(successMessage: successMessage);
            return result;
        }
    }

}
