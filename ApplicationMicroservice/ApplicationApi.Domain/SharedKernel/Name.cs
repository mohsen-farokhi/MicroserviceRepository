using System.Collections.Generic;

namespace ApplicationApi.Domain.SharedKernel
{
	public class Name : 
		Framework.Domain.SeedWork.ValueObject
	{
		#region Constant(s)
		public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static Framework.Result<Name> Create(string value)
		{
			var result =
				new Framework.Result<Name>();

			value =
				Framework.String.Fix(text: value);

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required,
					Resources.DataDictionary.Name);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength,
					Resources.DataDictionary.Name, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new Name(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private Name() : base()
		{
		}

		private Name(string value) : this()
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
			return Value;
		}
	}
}
