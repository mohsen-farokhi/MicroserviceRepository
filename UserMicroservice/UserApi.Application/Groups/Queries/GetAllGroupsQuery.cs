using System.Collections.Generic;
using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.Queries
{
    public class GetAllGroupsQuery :
        Framework.Mediator.IRequest<IList<GroupViewModel>>
    {
    }
}
