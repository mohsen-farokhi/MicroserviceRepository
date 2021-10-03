using System.Collections.Generic;

namespace UserApi.Domain.SharedKernel
{
    public class FirstName : 
		Framework.Domain.SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static Framework.Result<FirstName> Create(string value)
		{
			var result =
				new Framework.Result<FirstName>();

			value =
				Framework.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
                    Resources.DataDictionary.FirstName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength,
                    Resources.DataDictionary.FirstName, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new FirstName(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private FirstName() : base()
		{
		}

		private FirstName(string value) : this()
		{
			Value = value;
		}

		public string Value { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			if (string.IsNullOrWhiteSpace(Value))
			{
				return "-----";
			}
			else
			{
				return Value;
			}
		}
	}
}
