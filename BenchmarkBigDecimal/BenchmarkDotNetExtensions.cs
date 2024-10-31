// Copyright © the BigDecimal project created by https://github.com/AdamWhiteHat/.
//
// Project "BenchmarkBigDecimal", file “BenchmarkDotNetExtensions.cs” last formatted on 2024-10-03

namespace BenchmarkBigDecimal;

using System;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Properties;

public static class BenchmarkDotNetExtensions
{

    public static void ReportToConsole( this Summary? summary )
    {
        if ( summary is null )
        {
            String? message = LanguageResources.Null_summary;

            if ( message is not null )
            {
                Console.WriteLine( message );
            }

            return;
        }

        Console.WriteLine( summary.AllRuntimes );

        foreach ( BenchmarkCase? benchmarkCase in summary.BenchmarksCases )
        {
            Console.WriteLine( benchmarkCase );
        }
    }
}