namespace UserApi.Application.Users.Queries
{
    public class GetUserByIdQuery :
        Framework.Mediator.IRequest<ViewModels.Users.UserViewModel>
    {
        public int? Id { get; set; }
    }
}
