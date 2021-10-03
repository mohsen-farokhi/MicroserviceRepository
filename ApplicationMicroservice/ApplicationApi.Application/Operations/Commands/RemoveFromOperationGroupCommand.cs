namespace ApplicationApi.Application.Operations.Commands
{
    public class RemoveFromOperationGroupCommand :
        Framework.Mediator.IRequest
    {
        public int? OperationGroupId { get; set; }

        public int? OperationId { get; set; }
    }
}
