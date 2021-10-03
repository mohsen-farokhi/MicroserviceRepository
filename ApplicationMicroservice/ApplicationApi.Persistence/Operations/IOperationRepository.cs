using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.Operations;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Persistence.Operations
{
    public interface IOperationRepository :
        Framework.Persistence.IRepository<Operation>
    {
        Task<IList<OperationViewModel>> GetAllAsync
            (Application application, bool isActive);

        Task<OperationViewModel> GetByIdAsync
            (int id);

        Task<Operation> GetInclueOperationGroupAsync
            (int id);
    }
}
