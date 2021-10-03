using System.Collections.Generic;

namespace UserApi.Domain.SharedKernel
{
    public abstract class Date : 
		Framework.Domain.SeedWork.ValueObject
	{
		#region Static Member(s)
		public static bool operator <(Date left, Date right)
		{
			return left.Value < right.Value;
		}

		public static bool operator <=(Date left, Date right)
		{
			return left.Value <= right.Value;
		}

		public static bool operator >(Date left, Date right)
		{
			return left.Value > right.Value;
		}

		public static bool operator >=(Date left, Date right)
		{
			return left.Value >= right.Value;
		}
		#endregion /Static Member(s)

		protected Date() : base()
		{
		}

		protected Date(System.DateTime? value) : this()
		{
			if (value is not null)
			{
				value =
					value.Value.Date;
			}

			Value = value;
		}

		public System.DateTime? Value { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			if (Value is null)
			{
				return "-----";
			}

			string result =
				Value.Value.ToString("yyyy/MM/dd");

			return result;
		}
	}
}
