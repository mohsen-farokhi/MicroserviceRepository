namespace ApplicationApi.Persistence
{
	public class QueryUnitOfWork :
		 Framework.Persistence.EF.QueryUnitOfWork<QueryDatabaseContext>, IQueryUnitOfWork
	{
		public QueryUnitOfWork(QueryDatabaseContext databaseContext) : 
			base(databaseContext: databaseContext)
		{
		}
	}
}
