namespace UserApi.Application.Users.Commands
{
    public class AssignToGroupCommand:
        Framework.Mediator.IRequest
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
