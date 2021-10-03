namespace UserApi.Domain.Aggregates.Users.Events
{
	public sealed class UserEmailAddressChangedEvent :
		Framework.Domain.IDomainEvent
	{
		public UserEmailAddressChangedEvent
			(SharedKernel.FullName fullName, SharedKernel.EmailAddress emailAddress)
		{
			FullName = fullName;
			EmailAddress = emailAddress;
		}

		public SharedKernel.FullName FullName { get; }

		public SharedKernel.EmailAddress EmailAddress { get; }
	}
}
