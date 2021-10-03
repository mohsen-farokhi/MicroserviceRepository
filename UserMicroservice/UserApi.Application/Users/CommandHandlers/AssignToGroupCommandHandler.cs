using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Users.CommandHandlers
{
    public class AssignToGroupCommandHandler :
        Framework.Mediator.IRequestHandler<AssignToGroupCommand>
    {
        public AssignToGroupCommandHandler (IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (AssignToGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            // **************************************************
            if (request.UserId.HasValue == false ||
                request.GroupId.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedUser =
                await UnitOfWork.UserRepository.GetIncludeGroups(request.UserId.Value);

            if (foundedUser == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.User);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedGroup =
                await UnitOfWork.GroupRepository.FindAsync
                (request.GroupId.Value);

            if (foundedGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Group);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var addGroupResult =
                foundedUser.AssignToGroup(group: foundedGroup);

            if (addGroupResult.IsFailed)
            {
                result.WithErrors(addGroupResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.SaveAsync();

            var successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Group);

            result.WithSuccess(successMessage: successMessage);

            return result;
        }
    }
}
