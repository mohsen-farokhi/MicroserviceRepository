using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Groups.Queries;
using UserApi.Persistence;
using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.QueryHandlers
{
    public class GetGroupByIdQueryHandler :
        Framework.Mediator.IRequestHandler<GetGroupByIdQuery, GroupViewModel>
    {
        public GetGroupByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<GroupViewModel>> Handle
            (GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<GroupViewModel>();

            if (request.Id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);
                return result;
            }

            // **************************************************
            var foundedGroup = await UnitOfWork.GroupRepository.GetByIdAsync
                (request.Id.Value);

            if (foundedGroup == null)
            {
                var errprMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Group);

                result.WithError(errorMessage: errprMessage);

                return result;
            }
            // **************************************************

            result.WithValue(value: foundedGroup);

            return result;
        }
    }
}
