using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Persistence.Applications
{
    public interface IApplicationRepository :
        Framework.Persistence.IRepository<Application>
    {
        Task<ApplicationViewModel> GetByIdAsync
            (int id);

        Task<IList<ApplicationViewModel>> GetAllAsync
            (bool isActive);

    }
}
