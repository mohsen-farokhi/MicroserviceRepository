using System.Collections.Generic;
using System.Linq;
using ApplicationApi.Domain.Aggregates.Applications;
using ApplicationApi.Domain.Aggregates.OperationGroups;

namespace ApplicationApi.Domain.Aggregates.Operations
{
    public class Operation :
        Framework.Domain.SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static Framework.Result<Operation> Create
            (Application application,
            string name,
            string displayName,
            bool isActive,
            int accessType)
        {
            var result =
                new Framework.Result<Operation>();

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

            // **************************************************
            var displayNameResult =
                SharedKernel.Name.Create(value: displayName);

            if (displayNameResult.IsFailed)
            {
                result.WithErrors(errors: displayNameResult.Errors);
            }
            // **************************************************

            // **************************************************
            var accessTypeResult =
                ValueObjects.AccessType.GetByValue(value: accessType);

            if (accessTypeResult.IsFailed)
            {
                result.WithErrors(errors: accessTypeResult.Errors);
            }
            // **************************************************

            var operation = new Operation
                (application: application,
                name: nameResult.Value,
                displayName: displayNameResult.Value,
                isActive: isActive,
                accessType: accessTypeResult.Value);

            result.WithValue(value: operation);

            return result;
        }
        #endregion /Static Member(s)

        private Operation()
        {
        }

        private Operation
            (Application application,
            SharedKernel.Name name,
            SharedKernel.Name displayName,
            bool isActive,
            ValueObjects.AccessType accessType)
        {
            Application = application;
            Name = name;
            DisplayName = displayName;
            IsActive = isActive;
            AccessType = accessType;
        }

        public virtual Application Application { get; private set; }

        public SharedKernel.Name Name { get; private set; }

        public SharedKernel.Name DisplayName { get; private set; }

        public bool IsActive { get; private set; }

        public ValueObjects.AccessType AccessType { get; private set; }

        // **********
        private readonly List<OperationGroup> _operationGroups = new();

        public virtual IReadOnlyCollection<OperationGroup> OperationGroups
        {
            get
            {
                return _operationGroups;
            }
        }
        // **********

        public Framework.Result Update
            (Application application,
            string name,
            string displayName,
            bool isActive,
            int accessType)
        {
            // **************************************************
            var result = Create
                (application: application, name: name, displayName: displayName,
                isActive: isActive, accessType: accessType);

            if (result.IsFailed)
            {
                return result;
            }
            // **************************************************

            Application = result.Value.Application;
            Name = result.Value.Name;
            DisplayName = result.Value.DisplayName;
            IsActive = result.Value.IsActive;
            AccessType = result.Value.AccessType;

            return result;
        }

        public Framework.Result AssignToOperationGroup
            (OperationGroup operationGroup)
        {
            var result =
                new Framework.Result();

            // **************************************************
            if (operationGroup is null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var hasAny =
                _operationGroups.Any(c => c.Id == operationGroup.Id);

            if (hasAny)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Repetitive,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _operationGroups.Add(operationGroup);

            return result;
        }

        public Framework.Result RemoveFromOperationGroup
            (OperationGroup operationGroup)
        {
            var result =
                new Framework.Result();

            // **************************************************
            if (operationGroup is null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.Required,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            // **************************************************
            var foundedGroup =
                _operationGroups.FirstOrDefault(c => c.Id == operationGroup.Id);

            if (foundedGroup == null)
            {
                var errorMessage = string.Format
                    (Resources.Messages.Validations.NotFound,
                    Resources.DataDictionary.OperationGroup);

                result.WithError(errorMessage: errorMessage);

                return result;
            }
            // **************************************************

            _operationGroups.Remove(operationGroup);

            return result;
        }
    }
}
