using ApplicationApi.Persistence.Applications;
using ApplicationApi.Persistence.OperationGroups;
using ApplicationApi.Persistence.Operations;

namespace ApplicationApi.Persistence
{
    public class UnitOfWork :
        Framework.Persistence.EF.UnitOfWork<DatabaseContext>, IUnitOfWork
	{
		public UnitOfWork(DatabaseContext databaseContext) : 
			base(databaseContext: databaseContext)
		{
		}

		// **********
		private IApplicationRepository _applicationRepository;

		public IApplicationRepository ApplicationRepository
		{
			get
			{
				if (_applicationRepository == null)
				{
					_applicationRepository =
						new ApplicationRepository(databaseContext: DatabaseContext);
				}

				return _applicationRepository;
			}
		}
		// **********

		// **********
		private IOperationRepository _operationRepository;

		public IOperationRepository OperationRepository
		{
			get
			{
				if (_operationRepository == null)
				{
					_operationRepository =
						new OperationRepository(databaseContext: DatabaseContext);
				}

				return _operationRepository;
			}
		}
		// **********

		// **********
		private IOperationGroupRepository _operationGroupRepository;

		public IOperationGroupRepository OperationGroupRepository
		{

			get
			{
				if (_operationGroupRepository == null)
				{
					_operationGroupRepository =
						new OperationGroupRepository(databaseContext: DatabaseContext);
				}

				return _operationGroupRepository;
			}
		}
		// **********
	}
}
