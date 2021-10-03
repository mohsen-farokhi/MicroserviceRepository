namespace UserApi.Application.Users.Commands
{
    public class ChangePasswordCommand : 
        Framework.Mediator.IRequest
    {
        public int? Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
