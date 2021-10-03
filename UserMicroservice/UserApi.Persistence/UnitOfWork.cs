using UserApi.Persistence.Groups;
using UserApi.Persistence.Users;

namespace UserApi.Persistence
{
    public class UnitOfWork :
        Framework.Persistence.EF.UnitOfWork<DatabaseContext>, IUnitOfWork
	{
		public UnitOfWork(DatabaseContext databaseContext) : 
			base(databaseContext: databaseContext)
		{
		}

		// **********
		private IUserRepository _userRepository;

		public IUserRepository UserRepository
		{
			get
			{
				if (_userRepository == null)
				{
					_userRepository =
						new UserRepository(databaseContext: DatabaseContext);
				}

				return _userRepository;
			}
		}
		// **********

		// **********
		private IGroupRepository _groupRepository;

		public IGroupRepository GroupRepository
		{
			get
			{
				if (_groupRepository == null)
				{
					_groupRepository =
						new GroupRepository(databaseContext: DatabaseContext);
				}

				return _groupRepository;
			}
		}
		// **********

	}
}
