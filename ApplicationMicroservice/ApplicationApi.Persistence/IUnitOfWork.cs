using ApplicationApi.Persistence.Applications;
using ApplicationApi.Persistence.OperationGroups;
using ApplicationApi.Persistence.Operations;

namespace ApplicationApi.Persistence
{
    public interface IUnitOfWork :
        Framework.Persistence.IUnitOfWork
    {
        IApplicationRepository ApplicationRepository { get; }

        IOperationRepository OperationRepository { get; }

        IOperationGroupRepository OperationGroupRepository { get; }
    }
}
