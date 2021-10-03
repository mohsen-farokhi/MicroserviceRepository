namespace ApplicationApi.Domain.Aggregates.Operations.ValueObjects
{
    public class AccessType :
        Framework.Domain.SeedWork.Enumeration
    {
        #region Constant(s)
        public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static readonly AccessType Public = new(value: 0, name: Resources.DataDictionary.Public);
		public static readonly AccessType Registered = new(value: 1, name: Resources.DataDictionary.Registered);
		public static readonly AccessType Special = new(value: 2, name: Resources.DataDictionary.Special);

		public static Framework.Result<AccessType> GetByValue(int? value)
		{
			var result =
				new Framework.Result<AccessType>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
					Resources.DataDictionary.AccessType);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var accessType =
				FromValue<AccessType>(value: value.Value);

			if (accessType is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.InvalidCode,
					Resources.DataDictionary.AccessType);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			result.WithValue(accessType);

			return result;
		}
		#endregion /Static Member(s)

		private AccessType() : base()
		{
		}

		private AccessType(int value, string name) : base(value: value, name: name)
		{
		}
	}
}
