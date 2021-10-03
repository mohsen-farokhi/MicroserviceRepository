namespace UserApi.Application.Users.Commands
{
    public class RemoveFromGroupCommand :
        Framework.Mediator.IRequest
    {
        public int? UserId { get; set; }

        public int? GroupId { get; set; }
    }
}
