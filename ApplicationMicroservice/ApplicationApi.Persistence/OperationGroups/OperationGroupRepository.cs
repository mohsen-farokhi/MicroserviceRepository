using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.OperationGroups;
using ApplicationApi.ViewModels.OperationGroups;

namespace ApplicationApi.Persistence.OperationGroups
{
    public class OperationGroupRepository :
        Framework.Persistence.EF.Repository<OperationGroup>, IOperationGroupRepository
    {
        public OperationGroupRepository(DbContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IList<OperationGroupViewModel>> GetAllByApplicationIdAsync
            (Application application, bool isActive)
        {
            var operationGroups =
                await DbSet
                .Where(current => current.Application == application)
                .Where(current => current.IsActive == isActive)
                .Select(current => new OperationGroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    IsActive = current.IsActive,
                }).ToListAsync();

            return operationGroups;
        }

        public async Task<OperationGroupViewModel> GetByIdAsync
            (int id)
        {
            var operationGroup =
                await DbSet
                .Where(current => current.Id == id)
                .Select(current => new OperationGroupViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    IsActive = current.IsActive,
                }).FirstOrDefaultAsync();

            return operationGroup;
        }
    }
}
