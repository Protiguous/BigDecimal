using System;
using System.Numerics;
using ExtendedNumerics.Helpers;

namespace ExtendedNumerics
{

	public readonly partial /*record*/ struct BigDecimal
	{

		/// <summary>
		///     Primary constructor, accessed via the factory gatekeeper method <see cref="Create" />.
		/// </summary>
		/// <param name="normalize"></param>
		/// <param name="roundToPrecision"></param>
		/// <param name="precision"></param>
		/// <param name="exponent"></param>
		/// <param name="mantissa"></param>
		private BigDecimal(
			in NormalizeValue normalize,
			in bool roundToPrecision,
			in int precision,
			in int exponent,
#if NET5_0_OR_GREATER
			in
#endif
			BigInteger mantissa,
			in NormalizeValue normalizedAlreadyHint)
		{
			Mantissa = mantissa;
			Exponent = exponent;
			AtPrecision = precision;

			IsNormalized = normalizedAlreadyHint != NormalizeValue.No;

			if (roundToPrecision)
			{
				this = this.Round(precision);
			}

			if (normalize == NormalizeValue.Yes)
			{
				if (!IsNormalized)
				{
					this = this.Normalize();

					IsNormalized = true;
				}
				
			}

			if (Mantissa.IsZero)
			{
				//TODO Is this always desired?
				Exponent = 0;
			}

			BigInteger bigInteger = Mantissa;
			_significantDigitCount = new Lazy<int>(() => bigInteger.GetSignificantDigitsCount());
		}

		public BigDecimal(
			(BigInteger mantissa, int exponent) tuple,
			in NormalizeValue normalize = NormalizeValue.No)
		{
			this = Create(tuple.mantissa, tuple.exponent, normalize);
		}

		/// <summary>
		///     Factory gatekeeper method.
		/// </summary>
		/// <param name="mantissa"></param>
		/// <param name="exponent"></param>
		/// <param name="normalize"></param>
		/// <param name="alwaysTruncate"></param>
		/// <param name="precision"></param>
		/// <param name="normalizedAlreadyHint"></param>
		/// <returns></returns>
		public static BigDecimal Create(
#if NET5_0_OR_GREATER
			in
#endif
			BigInteger mantissa,
			in int exponent,
			in NormalizeValue normalize = NormalizeValue.No,
			bool? alwaysTruncate = null,
			int? precision = null,
			NormalizeValue normalizedAlreadyHint = NormalizeValue.No)
		{
			return new BigDecimal((normalize == NormalizeValue.Yes) || AlwaysNormalize ? NormalizeValue.Yes : NormalizeValue.No,
				alwaysTruncate ?? AlwaysTruncate, precision ?? Precision, exponent, mantissa, normalizedAlreadyHint);
		}

		public BigDecimal(string s, IFormatProvider? provider = null)
		{
			this = BigDecimal.Parse(s, provider);
		}
		
		

		public void Deconstruct(out BigInteger mantissa, out int exponent)
		{
			mantissa = Mantissa;
			exponent = Exponent;
		}
	}

}