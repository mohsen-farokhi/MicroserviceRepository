using System.Collections.Generic;
using System.Linq;

namespace UserApi.Domain.Aggregates.Users
{
    public class User :
        Framework.Domain.SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static Framework.Result<User> Create
            (string username, string password, string emailAddress,
            int? role, int? gender, string firstName, string lastName, string cellPhoneNumber)
        {
            var result =
                new Framework.Result<User>();

            // **************************************************
            var usernameResult =
                ValueObjects.Username.Create(value: username);

            result.WithErrors(errors: usernameResult.Errors);
            // **************************************************

            // **************************************************
            var passwordResult =
                ValueObjects.Password.Create(value: password);

            result.WithErrors(errors: passwordResult.Errors);
            // **************************************************

            // **************************************************
            var emailAddressResult =
                SharedKernel.EmailAddress.Create(value: emailAddress);

            result.WithErrors(errors: emailAddressResult.Errors);
            // **************************************************

            // **************************************************
            var roleResult =
                ValueObjects.Role.GetByValue(value: role);

            result.WithErrors(errors: roleResult.Errors);
            // **************************************************

            // **************************************************
            var fullNameResult =
                SharedKernel.FullName.Create
                (gender: gender, firstName: firstName, lastName: lastName);

            result.WithErrors(errors: fullNameResult.Errors);
            // **************************************************

            // **************************************************
            var cellPhoneNumberResult =
                SharedKernel.CellPhoneNumber.Create(value: cellPhoneNumber);

            result.WithErrors(errors: cellPhoneNumberResult.Errors);
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var user =
                new User(username: usernameResult.Value, password: passwordResult.Value,
                emailAddress: emailAddressResult.Value, role: roleResult.Value,
                fullName: fullNameResult.Value, cellPhoneNumber: cellPhoneNumberResult.Value);

            result.WithValue(value: user);

            return result;
        }
        #endregion /Static Member(s)

        private User()
        {
        }

        private User
            (ValueObjects.Username username,
            ValueObjects.Password password,
            ValueObjects.Role role,
            SharedKernel.FullName fullName,
            SharedKernel.EmailAddress emailAddress,
            SharedKernel.CellPhoneNumber cellPhoneNumber)
        {
            Role = role;
            Username = username;
            Password = password;
            FullName = fullName;
            EmailAddress = emailAddress;
            CellPhoneNumber = cellPhoneNumber;
        }

        public ValueObjects.Role Role { get; private set; }

        public ValueObjects.Username Username { get; private set; }

        public ValueObjects.Password Password { get; private set; }

        public SharedKernel.FullName FullName { get; private set; }

        public SharedKernel.EmailAddress EmailAddress { get; private set; }

        public SharedKernel.CellPhoneNumber CellPhoneNumber { get; private set; }

        // **********
        private readonly List<Groups.Group> _groups = new();

        public virtual IReadOnlyList<Groups.Group> Groups
        {
            get
            {
                return _groups;
            }
        }
        // **********

        public Framework.Result Update(string username, string password, string emailAddress,
            int? role, int? gender, string firstName, string lastName, string cellPhoneNumber)
        {
            var result =
                Create(username: username, password: password, emailAddress: emailAddress,
                role: role, gender: gender, firstName: firstName, lastName: lastName,
                cellPhoneNumber: cellPhoneNumber);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            Role = result.Value.Role;
            Username = result.Value.Username;
            Password = result.Value.Password;
            FullName = result.Value.FullName;
            EmailAddress = result.Value.EmailAddress;
            CellPhoneNumber = result.Value.CellPhoneNumber;

            return result;
        }

        public Framework.Result ChangePassword
            (string newPassword, string confirmNewPassword)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var newPasswordResult =
                ValueObjects.Password.Create(value: newPassword);

            if (newPasswordResult.IsFailed)
            {
                result.WithErrors(errors: newPasswordResult.Errors);
                return result;
            }
            // **************************************************

            // **************************************************
            var confirmNewPasswordResult =
                ValueObjects.Password.Create(value: confirmNewPassword);

            if (confirmNewPasswordResult.IsFailed)
            {
                result.WithErrors(errors: confirmNewPasswordResult.Errors);
                return result;
            }
            // **************************************************

            // **************************************************
            if (newPasswordResult.Value != confirmNewPasswordResult.Value)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.RegularExpression,
                    Resources.DataDictionary.ConfirmPassword);

                result.WithError(errorMessage: errorMessage);
                return result;
            }
            // **************************************************

            Password = newPasswordResult.Value;

            RaiseDomainEvent
                (new Events.UserPasswordChangedEvent
                (fullName: FullName, emailAddress: EmailAddress));

            return result;
        }


        public Framework.Result AssignToGroup(Groups.Group group)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var hasAny = _groups.Any(c => c.Id == group.Id);

            if (hasAny)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.Group);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _groups.Add(group);

            return result;
        }

        public Framework.Result RemoveFromGroup(Groups.Group group)
        {
            var result =
                new Framework.Result();

            // **************************************************
            var foundedGroup = 
                _groups.FirstOrDefault(c => c.Id == group.Id);

            if (foundedGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.Group);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _groups.Remove(foundedGroup);

            return result;
        }

    }
}
