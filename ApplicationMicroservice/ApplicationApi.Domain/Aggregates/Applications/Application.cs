using System.Collections.Generic;
using ApplicationApi.Domain.Aggregates.OperationGroups;
using ApplicationApi.Domain.Aggregates.Operations;

namespace ApplicationApi.Domain.Aggregates.Applications
{
    public class Application :
        Framework.Domain.SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static Framework.Result<Application> Create
            (string name, string displayName, bool isActive)
        {
            var result =
                new Framework.Result<Application>();

            // **************************************************
            var nameResult =
                SharedKernel.Name.Create(value: name);

            if (nameResult.IsFailed)
            {
                result.WithErrors(errors: nameResult.Errors);
            }
            // **************************************************

            // **************************************************
            var displayNameResult =
                SharedKernel.Name.Create(value: displayName);

            if (displayNameResult.IsFailed)
            {
                result.WithErrors(errors: displayNameResult.Errors);
            }
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var application = new Application
                (name: nameResult.Value,
                displayName: displayNameResult.Value,
                isActive: isActive);

            result.WithValue(value: application);

            return result;
        }
        #endregion /Static Member(s)

        private Application()
        {
        }

        private Application
            (SharedKernel.Name name, SharedKernel.Name displayName, bool isActive)
        {
            Name = name;
            DisplayName = displayName;
            IsActive = isActive;
        }

        public SharedKernel.Name Name { get; private set; }

        public SharedKernel.Name DisplayName { get; private set; }

        public bool IsActive { get; private set; }

        // **********
        private readonly List<OperationGroup> _operationGroups = new();

        public virtual IReadOnlyList<OperationGroup> OperationGroups
        {
            get
            {
                return _operationGroups;
            }
        }
        // **********

        // **********
        private readonly List<Operation> _operations = new();

        public virtual IReadOnlyList<Operation> Operations
        {
            get
            {
                return _operations;
            }
        }
        // **********

        public Framework.Result<Application> Update
            (string name, string displayName, bool isActive)
        {
            // **************************************************
            var result =
                Create(name: name, displayName: displayName, isActive: isActive);

            if (result.IsFailed)
            {
                return result;
            }
            // **************************************************

            Name = result.Value.Name;
            DisplayName = result.Value.DisplayName;
            IsActive = result.Value.IsActive;

            return result;
        }
    }
}
