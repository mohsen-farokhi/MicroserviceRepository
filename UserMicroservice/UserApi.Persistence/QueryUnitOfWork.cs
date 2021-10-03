namespace UserApi.Persistence
{
	public class QueryUnitOfWork :
		 Framework.Persistence.EF.QueryUnitOfWork<QueryDatabaseContext>, IQueryUnitOfWork
	{
		public QueryUnitOfWork(QueryDatabaseContext databaseContext) : 
			base(databaseContext: databaseContext)
		{
		}

		// **********
		private Users.IQueryUserRepository _userRepository;
		public Users.IQueryUserRepository UserRepository
		{
			get
			{
				if (_userRepository == null)
				{
					_userRepository =
						new Users.QueryUserRepository(databaseContext: DatabaseContext);
				}

				return _userRepository;
			}
		}
		// **********
	}
}
