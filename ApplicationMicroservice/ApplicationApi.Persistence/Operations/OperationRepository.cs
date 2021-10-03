using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.Operations;
using ApplicationApi.ViewModels.Operations;

namespace ApplicationApi.Persistence.Operations
{
    public class OperationRepository :
        Framework.Persistence.EF.Repository<Operation>, IOperationRepository
    {
        public OperationRepository(DbContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IList<OperationViewModel>> GetAllAsync
            (Application application, bool isActive)
        {
            var operations =
                await DbSet
                .Where(current => current.Application == application)
                .Where(current => current.IsActive == isActive)
                .Select(current => new OperationViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    DisplayName = current.DisplayName.Value,
                    IsActive = current.IsActive,
                }).ToListAsync();

            return operations;
        }

        public async Task<OperationViewModel> GetByIdAsync
            (int id)
        {
            var operation =
                await DbSet
                .Where(current => current.Id == id)
                .Select(current => new OperationViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    DisplayName = current.DisplayName.Value,
                    IsActive = current.IsActive,
                }).FirstOrDefaultAsync();

            return operation;
        }

        public async Task<Operation> GetInclueOperationGroupAsync
            (int id)
        {
            var operation =
                await DbSet
                .Include(current => current.OperationGroups)
                .Where(current => current.Id == id)
                .FirstOrDefaultAsync();

            return operation;
        }

    }
}
