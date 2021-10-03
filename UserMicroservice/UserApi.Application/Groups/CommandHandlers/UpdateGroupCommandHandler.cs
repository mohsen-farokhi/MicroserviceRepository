using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Groups.Commands;
using UserApi.Persistence;

namespace UserApi.Application.Groups.CommandHandlers
{
    public class UpdateGroupCommandHandler :
        Framework.Mediator.IRequestHandler<UpdateGroupCommand>
    {
        public UpdateGroupCommandHandler
            (IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result> Handle
            (UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var foundedGroup = await UnitOfWork.GroupRepository.FindAsync
                (id: request.Id);

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
            var groupResult = foundedGroup.Update
                (name: request.Name, isActive: request.IsActive);

            if (groupResult.IsFailed)
            {
                result.WithErrors(errors: groupResult.Errors);

                return result;
            }
            // **************************************************

            await UnitOfWork.GroupRepository.UpdateAsync
                (entity: groupResult.Value);

            await UnitOfWork.SaveAsync();

            return result;
        }
    }
}
