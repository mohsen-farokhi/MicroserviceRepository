using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.ViewModels.Applications;

namespace ApplicationApi.Persistence.Applications
{
    public class ApplicationRepository :
        Framework.Persistence.EF.Repository<Application>, IApplicationRepository
    {
        public ApplicationRepository
            (DbContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ApplicationViewModel> GetByIdAsync(int id)
        {
            var application =
                await DbSet
                .Where(current => current.Id == id)
                .Select(current => new ApplicationViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    DisplayName = current.DisplayName.Value,
                    IsActive = current.IsActive,
                }).FirstOrDefaultAsync();

            return application;
        }

        public async Task<IList<ApplicationViewModel>> GetAllAsync(bool isActive)
        {
            var applications =
                await DbSet
                .Where(current => current.IsActive == isActive)
                .Select(current => new ApplicationViewModel
                {
                    Id = current.Id,
                    Name = current.Name.Value,
                    DisplayName = current.DisplayName.Value,
                    IsActive = current.IsActive,
                }).ToListAsync();

            return applications;
        }

    }
}
