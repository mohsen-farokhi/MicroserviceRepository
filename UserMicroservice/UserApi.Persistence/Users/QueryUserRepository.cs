using Framework.Persistence.EF;
using Framework.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserApi.ViewModels.Users;

namespace UserApi.Persistence.Users
{
    public class QueryUserRepository : 
        QueryRepository<Domain.Aggregates.Users.User>, IQueryUserRepository
    {
        public QueryUserRepository(QueryDatabaseContext databaseContext) :
            base(databaseContext: databaseContext)
        {
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user =
                await DbSet
                .AsNoTracking()
                .Where(current => current.Id == id)
                .Select(current => new UserViewModel
                {
                    Id = current.Id,
                    EmailAddress = current.EmailAddress.Value,
                    FirstName = current.FullName.FirstName.Value,
                    LastName = current.FullName.LastName.Value,
                    Role = current.Role.Value,
                    Gender = current.FullName.Gender.Value,
                }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<ViewPagingDataResult<UserViewModel>>
            GetAllAsync(GetAllUserRequestViewModel request)
        {
            var users =
                DbSet
                .AsNoTracking()
                .AsQueryable();

            var result = new ViewPagingDataResult<UserViewModel>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount =
                    request.TotalCount != 0 ?
                    request.TotalCount :
                    await users.CountAsync(),
                Result =
                    await users
                    .Skip(request.PageSize * request.PageIndex)
                    .Take(request.PageSize)
                    .OrderByDescending(current => current.Id)
                    .Select(current => new UserViewModel
                    {
                        Id = current.Id,
                        FirstName = current.FullName.FirstName.Value,
                        LastName = current.FullName.LastName.Value,
                        EmailAddress = current.EmailAddress.Value,
                        Gender = current.FullName.Gender.Value,
                        Role = current.Role.Value,
                        CellPhoneNumber = current.CellPhoneNumber.Value,
                    }).ToListAsync()
            };

            return result;
        }
    }
}
