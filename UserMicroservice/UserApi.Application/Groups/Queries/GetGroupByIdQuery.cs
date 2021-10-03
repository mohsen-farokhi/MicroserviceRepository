using UserApi.ViewModels.Groups;

namespace UserApi.Application.Groups.Queries
{
    public class GetGroupByIdQuery :
        Framework.Mediator.IRequest<GroupViewModel>
    {
        public int? Id { get; set; }
    }
}
