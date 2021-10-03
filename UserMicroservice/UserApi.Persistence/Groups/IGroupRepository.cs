using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Domain.Aggregates.Groups;
using UserApi.ViewModels.Groups;

namespace UserApi.Persistence.Groups
{
    public interface IGroupRepository :
        Framework.Persistence.IRepository<Group>
    {
        Task<IList<GroupViewModel>> GetAllAsync();

        Task<GroupViewModel> GetByIdAsync(int id);
    }
}
