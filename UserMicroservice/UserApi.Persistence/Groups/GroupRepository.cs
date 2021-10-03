using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Domain.Aggregates.Groups;
using UserApi.ViewModels.Groups;

namespace UserApi.Persistence.Groups
{
    public class GroupRepository :
        Framework.Persistence.EF.Repository<Group>, IGroupRepository
    {
        public GroupRepository(DbContext databaseContext) :
            base(databaseContext)
        {
        }

        public async Task<IList<GroupViewModel>> GetAllAsync()
        {
            var groups =
                await DbSet
                .Select(current => new GroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    IsActive = current.IsActive,
                }).ToListAsync();

            return groups;
        }

        public async Task<GroupViewModel> GetByIdAsync(int id)
        {
            var group =
                await DbSet
                .Where(current => current.Id == id)
                .Select(current => new GroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    IsActive = current.IsActive,
                }).FirstOrDefaultAsync();

            return group;
        }

    }
}
