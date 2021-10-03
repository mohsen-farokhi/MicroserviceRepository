using System.Collections.Generic;
using System.Linq;

namespace UserApi.Domain.Aggregates.Groups
{
    public class Group :
        Framework.Domain.SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static Framework.Result<Group> Create
            (string name, bool isActive)
        {
            var result =
                new Framework.Result<Group>();

            // **************************************************
            var nameResult =
                SharedKernel.Name.Create(value: name);

            if (nameResult.IsFailed)
            {
                result.WithErrors(errors: nameResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var group =
                new Group(name: nameResult.Value, isActive: isActive);

            result.WithValue(value: group);

            return result;
        }
        #endregion /Static Member(s)

        private Group()
        {
        }

        private Group
            (SharedKernel.Name name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        public SharedKernel.Name Name { get; set; }

        public bool IsActive { get; set; }

        // **********
        private readonly List<Users.User> _users = new();

        public virtual IReadOnlyList<Users.User> Users
        {
            get
            {
                return _users;
            }
        }
        // **********

        public Framework.Result<Group> Update
            (string name, bool isActive)
        {
            // **************************************************
            var result =
                Create(name: name, isActive: isActive);

            if (result.IsFailed)
            {
                return result;
            }
            // **************************************************

            Name = result.Value.Name;
            IsActive = result.Value.IsActive;

            return result;
        }

    }
}
