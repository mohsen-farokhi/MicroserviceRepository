using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Domain.Aggregates.Users;
using UserApi.Domain.Aggregates.Users.ValueObjects;
using UserApi.ViewModels.Users;

namespace UserApi.Persistence.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) :
            base(databaseContext: databaseContext)
        {
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user =
                await DbSet
                .Where(current => current.Id == id)
                .Select(current => new UserViewModel
                {
                    Id = current.Id,
                    FirstName = current.FullName.FirstName.Value,
                    LastName = current.FullName.LastName.Value,
                    Gender = current.FullName.Gender.Value,
                    CellPhoneNumber = current.CellPhoneNumber.Value,
                    EmailAddress = current.EmailAddress.Value,
                    Role = current.Role.Value,
                }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<UserViewModel>
            GetByUsernameAsync(Username username)
        {
            var user =
                await DbSet
                .Where(current => current.Username == username)
                .Select(current => new ViewModels.Users.UserViewModel
                {
                    Id = current.Id,
                    FirstName = current.FullName.FirstName.Value,
                    LastName = current.FullName.LastName.Value,
                    Role = current.Role.Value,
                    CellPhoneNumber = current.CellPhoneNumber.Value,
                    EmailAddress = current.EmailAddress.Value,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetIncludeGroups(int id)
        {
            var user =
                await DbSet
                .Where(current => current.Id == id)
                .Include(current => current.Groups)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
