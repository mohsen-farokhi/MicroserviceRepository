using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Users.Queries;
using UserApi.Persistence;
using UserApi.ViewModels.Users;

namespace UserApi.Application.Users.QueryHandlers
{
    public class GetUserByIdQueryHandler :
        Framework.Mediator.IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        public GetUserByIdQueryHandler
            (IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<UserViewModel>> Handle
            (GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<UserViewModel>();

            if (request.Id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.Messages.Errors.BadRequest);
                return result;
            }

            // **************************************************
            var user =
                await UnitOfWork.UserRepository.GetByIdAsync(request.Id.Value);

            if (user == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.User);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            result.WithValue(value: user);

            return result;
        }
    }
}
