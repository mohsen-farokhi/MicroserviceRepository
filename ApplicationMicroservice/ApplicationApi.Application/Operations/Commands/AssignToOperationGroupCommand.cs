namespace ApplicationApi.Application.Operations.Commands
{
    public class AssignToOperationGroupCommand:
        Framework.Mediator.IRequest
    {
        public int? OperationId { get; set; }

        public int? OperationGroupId { get; set; }
    }
}
