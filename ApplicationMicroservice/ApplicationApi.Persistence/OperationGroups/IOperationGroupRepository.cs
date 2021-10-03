using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.OperationGroups;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Persistence.OperationGroups
{
    public interface IOperationGroupRepository :
        Framework.Persistence.IRepository<OperationGroup>
    {
        Task<IList<OperationGroupViewModel>> GetAllByApplicationIdAsync
            (Application application, bool isActive);
        Task<OperationGroupViewModel> GetByIdAsync
            (int id);
    }
}
