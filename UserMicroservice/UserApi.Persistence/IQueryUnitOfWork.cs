namespace UserApi.Persistence
{
	public interface IQueryUnitOfWork : 
		Framework.Persistence.IQueryUnitOfWork
	{
		Users.IQueryUserRepository UserRepository { get; }
	}
}
