// Copyright © the BigDecimal project created by https://github.com/AdamWhiteHat/.
//
// Project "BenchmarkBigDecimal", file “Program.cs” last formatted on 2024-10-03

namespace BenchmarkBigDecimal;

using System;
using System.Diagnostics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

// BenchmarkDotNet is not supported under NET45.
// The page https://benchmarkdotnet.org/articles/faq.html says that NET461 or higher is required.

public static class Program
{
    [Conditional( "RELEASE" )]
    public static void Main( String[] args )
    {
        IConfig config = DefaultConfig.Instance;
        Summary summary = BenchmarkRunner.Run<CreatingBigDecimals>( config, args );

        summary.ReportToConsole();
    }
}