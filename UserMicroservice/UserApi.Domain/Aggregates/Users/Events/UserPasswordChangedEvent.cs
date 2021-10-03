namespace UserApi.Domain.Aggregates.Users.Events
{
    public sealed class UserPasswordChangedEvent :
        Framework.Domain.IDomainEvent
    {
        public UserPasswordChangedEvent
            (SharedKernel.FullName fullName, SharedKernel.EmailAddress emailAddress)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
        }

        public SharedKernel.FullName FullName { get; }

        public SharedKernel.EmailAddress EmailAddress { get; }
    }
}
