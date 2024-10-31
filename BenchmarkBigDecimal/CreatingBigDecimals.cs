// Copyright © the BigDecimal project created by https://github.com/AdamWhiteHat/.
//
// Project "BenchmarkBigDecimal", file “CreatingBigDecimals.cs” last formatted on 2024-10-03

namespace BenchmarkBigDecimal;

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using ExtendedNumerics;

/// <summary>
///     Benchmark various <see cref="BigDecimal" />.
/// </summary>
/// <remarks>Written by Protiguous.</remarks>
[Orderer( SummaryOrderPolicy.FastestToSlowest )]
[RankColumn( NumeralSystem.Arabic )]
[MemoryDiagnoser( true )]
public class CreatingBigDecimals
{

    private const Int32 Kilo = 1024;

    private static readonly Random RNG = new Random();

    [ParamsSource( nameof( Sizes ) )]
    public Int32 Digits { get; set; }

    /// <summary>
    ///     Use [ParamsSource(nameof( Sizes ))] on variable for use inside a [Benchmark].
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Object> Sizes()
    {
        const Int32 min = 1;
        const Int32 max = 64 * Kilo;
        const Int32 length = max - min;
        const Int32 chunks = 8;
        const Int32 chunksize = length / chunks;

        for ( Int32 i = min; i < max; i += chunksize )
        {
            yield return i;
        }
    }

    [Benchmark( Baseline = true )]
    public BigDecimal BigDecimal_CreateViaNew()
    {
        Byte[] buffer = ArrayPool<Byte>.Shared.Rent( this.Digits );

        try
        {
            BigInteger mantissa = new BigInteger( buffer );

            Int32 exponent = RNG.Next( Int32.MinValue, Int32.MaxValue );

            BigDecimal result = new BigDecimal( mantissa, exponent );

            return result;
        }
        finally
        {
            ArrayPool<Byte>.Shared.Return( buffer );
        }
    }
}