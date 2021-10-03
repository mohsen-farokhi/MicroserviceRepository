using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Groups.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Groups.CommandHandlers
{
    public class CreateGroupCommandHandler :
        Framework.Mediator.IRequestHandler<CreateGroupCommand, int>
    {
        public CreateGroupCommandHandler
            (IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<int>> Handle
            (CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<int>();

            // **************************************************
            var groupResult = Domain.Aggregates.Groups.Group.Create
                (name: request.Name, isActive: request.IsActive);

            if (groupResult.IsFailed)
            {
                result.WithErrors(errors: groupResult.Errors);

                return result;
            }
            // **************************************************

            var group = await UnitOfWork.GroupRepository.AddAsync
                (entity: groupResult.Value);

            await UnitOfWork.SaveAsync();

            string successMessage = string.Format
                (Resources.Messages.Successes.SuccessCreate,
                Resources.DataDictionary.Group);

            result.WithSuccess(successMessage: successMessage);

            result.WithValue(value: group.Id);

            return result;
        }
    }
}
