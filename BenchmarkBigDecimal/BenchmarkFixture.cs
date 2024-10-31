// Copyright © the BigDecimal project created by https://github.com/AdamWhiteHat/.
//
// Project "BenchmarkBigDecimal", file “BenchmarkFixture.cs” last formatted on 2024-10-03

namespace Benchmarking;

using BenchmarkBigDecimal;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

public class BenchmarkFixture
{

    public BenchmarkFixture()
    {
        ManualConfig config = new ManualConfig
        {
            SummaryStyle = SummaryStyle.Default.WithMaxParameterColumnWidth( 100 ),
            Orderer = new DefaultOrderer( SummaryOrderPolicy.FastestToSlowest ),
            Options = ConfigOptions.Default
        };

        this.BenchmarkSummary = BenchmarkRunner.Run<CreatingBigDecimals>( config );
    }

    public Summary BenchmarkSummary { get; }
}