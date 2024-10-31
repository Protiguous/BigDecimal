namespace ExtendedNumerics
{

	/// <summary>
	///     <para>
	///         Specify if the <see cref="BigDecimal" /> should be run through the
	///         <see cref="BigDecimalExtensions.Normalize" /> function when created.
	///     </para>
	/// </summary>
	public enum NormalizeValue
	{

		/// <summary>
		///     The <see cref="BigDecimal" /> has not been run through <see cref="BigDecimalExtensions.Normalize" />, or will not
		///     be run <see cref="BigDecimalExtensions.Normalize" /> when created.
		/// </summary>
		No,

		/*
		/// <summary>
		///     The default, which is <see cref="No" />.
		/// </summary>
		Default = No,
		*/

		/// <summary>
		///     <para><see cref="BigDecimalExtensions.Normalize" /> will be run or has been run.</para>
		/// </summary>
		Yes //,

		/*
		/// <summary>
		///     Same as <see cref="Yes" />.
		/// </summary>
		Auto = Yes
		*/

	}

}