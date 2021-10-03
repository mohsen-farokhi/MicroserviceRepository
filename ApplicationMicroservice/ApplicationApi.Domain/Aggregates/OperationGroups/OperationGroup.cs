using System.Collections.Generic;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.Operations;

namespace ApplicationApi.Domain.Aggregates.OperationGroups
{
    public class OperationGroup :
        Framework.Domain.SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static Framework.Result<OperationGroup> Create
            (Application application, string name, bool isActive)
        {
            var result =
                new Framework.Result<OperationGroup>();

            // **************************************************
            if (application is null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

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

            var operationGroup =
                new OperationGroup
                (application: application,
                name: nameResult.Value,
                isActive: isActive);

            result.WithValue(value: operationGroup);

            return result;

        }
        #endregion /Static Member(s)

        private OperationGroup()
        {
        }

        private OperationGroup
            (Application application, SharedKernel.Name name, bool isActive)
        {
            Application = application;
            Name = name;
            IsActive = isActive;
        }

        public virtual Application Application { get; private set; }

        public SharedKernel.Name Name { get; private set; }

        public bool IsActive { get; private set; }


        // **********
        private readonly List<Operation> _operations = new();

        public virtual IReadOnlyCollection<Operation> Operations
        {
            get
            {
                return _operations;
            }
        }
        // **********

        public Framework.Result<OperationGroup> Update
            (Application application, string name, bool isActive)
        {
            var result =
                Create(application: application, name: name, isActive: isActive);

            if (result.IsFailed)
            {
                return result;
            }

            Application = result.Value.Application;
            Name = result.Value.Name;
            IsActive = result.Value.IsActive;

            return result;
        }

    }
}
