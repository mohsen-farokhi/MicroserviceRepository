namespace UserApi.Domain.Aggregates.Users.ValueObjects
{
    public class Role :
		Framework.Domain.SeedWork.Enumeration
    {
        #region Constant(s)
        public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static readonly Role Customer = new(value: 0, name: Resources.DataDictionary.Customer);
		public static readonly Role Agent = new(value: 1, name: Resources.DataDictionary.Agent);
		public static readonly Role Supervisor = new(value: 2, name: Resources.DataDictionary.Supervisor);
		public static readonly Role Administrator = new(value: 3, name: Resources.DataDictionary.Administrator);
		public static readonly Role Programmer = new(value: 4, name: Resources.DataDictionary.Programmer);

		public static Framework.Result<Role> GetByValue(int? value)
		{
			var result =
				new Framework.Result<Role>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
                    Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var role =
				FromValue<Role>(value: value.Value);

			if (role is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.InvalidCode,
                    Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			result.WithValue(role);

			return result;
		}
		#endregion /Static Member(s)

		private Role() : base()
		{
		}

		private Role(int value, string name) : base(value: value, name: name)
		{
		}

	}
}
