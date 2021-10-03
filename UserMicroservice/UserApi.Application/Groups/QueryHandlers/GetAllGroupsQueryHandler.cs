using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Groups.Queries;
using UserApi.Persistence;
using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.QueryHandlers
{
    public class GetAllGroupsQueryHandler :
        Framework.Mediator.IRequestHandler<GetAllGroupsQuery, IList<GroupViewModel>>
    {
        public GetAllGroupsQueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }

        public async Task<Framework.Result<IList<GroupViewModel>>> Handle
            (GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var result =
                new Framework.Result<IList<GroupViewModel>>();

            var groups =
                await UnitOfWork.GroupRepository.GetAllAsync();

            result.WithValue(value: groups);

            return result;
        }
    }
}
