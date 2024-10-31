using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using ExtendedNumerics.Helpers;

namespace ExtendedNumerics
{

	public static class BigDecimalExtensions
	{

		[DebuggerStepThrough]
		public static BigDecimal ChangePrecision(in this BigDecimal value, in int precision, in NormalizeValue normalize = NormalizeValue.No )
		{
			return BigDecimal.Create(value.Mantissa, value.Exponent, normalize, null, precision);
		}

		/// <summary>
		///     Removes any trailing zeros on the mantissa, adjusts the exponent, and returns a new <see cref="BigDecimal" />.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static BigDecimal Normalize(in this BigDecimal value)
		{
			if (value.IsNormalized)
			{
				return value;
			}

			//TODO Pull over changes from Experimental branch.

			

			if (value.Mantissa.IsZero)
			{
				if (value.Exponent != 0)
				{
					return BigDecimal.Zero;
				}

				//return value;
				return BigDecimal.Create(value.Mantissa, value.Exponent, NormalizeValue.No, null, null, NormalizeValue.Yes);
			}

			var s = value.Mantissa.ToString();
			
			int index = s.Length - 1;
			var pos = s.LastIndexOf('0', index);

			if (pos < index)
			{
				//return value;
				return BigDecimal.Create(value.Mantissa, value.Exponent, NormalizeValue.No, null, null, NormalizeValue.Yes);
			}

			var c = s[pos];

			while ((pos > 0) && (c == '0'))
			{
				c = s[--pos]; //scan backwards to find the last non0.
			}

			int startIndex = pos + 1;
			
			var mant = s.Substring(0, startIndex);
			var zeros = s.Substring(startIndex);

			if (BigInteger.TryParse(mant, out var mantissa))
			{
				var bigDecimal = BigDecimal.Create(mantissa, value.Exponent + zeros.Length, NormalizeValue.No, null,null, NormalizeValue.Yes);

				return bigDecimal;
			}

			//return value;
			return BigDecimal.Create(value.Mantissa, value.Exponent, NormalizeValue.No, null, null, NormalizeValue.Yes);
		}

		/// <summary>
		///     Returns the number of digits to the left of the decimal point.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int PlacesLeftOfDecimal(in this BigDecimal value)
		{
			int leftSideOfDecimal = value.Mantissa.NumberOfDigits() + value.Exponent;

			return leftSideOfDecimal;
		}

		/// <summary>
		///     Returns the number of digits to the right of the decimal point.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int PlacesRightOfDecimal(in this BigDecimal value)
		{
			int rightSideOfDecimal = value.Mantissa.NumberOfDigits() - value.PlacesLeftOfDecimal();

			return rightSideOfDecimal;
		}

		/// <summary>
		///     Returns the mantissa of value, aligned to the exponent of reference.
		///     <para>Assumes the exponent of <paramref name="value" /> is larger than of <paramref name="reference" />.</para>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="reference"></param>
		/// <returns></returns>
		public static BigInteger AlignExponent(in this BigDecimal value, in BigDecimal reference)
		{
			int difference = value.Exponent - reference.Exponent;

			return value.Mantissa * BigInteger.Pow(BigIntegerHelper.Ten, difference);
		}

		/// <summary>
		///     Rounds a BigDecimal to the given number of digits to the right of the decimal point.
		///     <para>
		///         Pass a negative precision value to round (zero) digits to the left of the decimal point in a manner that
		///         mimics Excel's ROUNDDOWN function.
		///     </para>
		/// </summary>
		public static BigDecimal Round(in this BigDecimal value, in int precision)
		{
			if (precision == 0)
			{
				return value.WholeValue;
			}

			if (precision < 0)
			{
				string integers = value.WholeValue.ToString() ?? string.Empty;
				int len = integers.Length;

				if (Math.Abs(precision) >= len)
				{
					return BigDecimal.Zero;
				}

				int diff = len + precision;

				string result = integers.Substring(0, diff);
				result += new string(Enumerable.Repeat('0', Math.Abs(precision)).ToArray());

				return BigDecimal.Parse(result);
			}

			BigInteger mantissa = value.Mantissa;
			int exponent = value.Exponent;
			int sign = Math.Sign(exponent);
			int digits = PlacesRightOfDecimal(value);

			if (digits > precision)
			{
				int difference = digits - precision;
				mantissa = BigInteger.Divide(mantissa, BigInteger.Pow(BigIntegerHelper.Ten, difference));

				if (sign != 0)
				{
					exponent -= sign * difference;
				}
			}

			return new BigDecimal(mantissa, exponent);
		}
	}

}