using System;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Threading;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigDecimal
{

	[NonParallelizable]
	[TestFixture]
	[Culture("en-US,ru-RU")]
	public static class TestBigDecimalCritical
	{

		private static NumberFormatInfo _format => Thread.CurrentThread.CurrentCulture.NumberFormat;

		[Test]
		public static void Test47()
		{
			BigDecimal π1 = BigDecimal.π * 1;
			BigDecimal π2 = BigDecimal.π * 2;
			BigDecimal π4 = BigDecimal.π * 4;
			BigDecimal π8 = BigDecimal.π * 8;
			
			BigDecimal sum = π1 + π2 + π4 + π8;
			BigInteger actual = sum.WholeValue;
		
			BigInteger expected = 47;

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestConstructor0EqualsNew0()
		{
			BigDecimal expected = 0;
			BigDecimal actual = new BigDecimal(0);

			Assert.True(BigDecimal.Equals(expected, actual));
		}
		
		[Test]
		public static void TestConstructorZeroVsNew0()
		{
			BigDecimal expected = BigDecimal.Zero;
			BigDecimal actual = new BigDecimal(0);

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		[Culture("en-US")]
		public static void TestConstructorZeroVsStringZero()
		{
			string expected = "0";
			string? actual = BigDecimal.Zero.ToString();

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestConstructorMantissa0Exp0()
		{
			BigDecimal expected = BigDecimal.Zero;
			BigDecimal actual = new BigDecimal(0, 0);

			Assert.True(BigDecimal.Equals(expected, actual));
			//Assert.AreEqual("0", actual.ToString());
		}

		[Test]
		public static void TestConstructorMantissa0Exp1()
		{
			BigDecimal expected = BigDecimal.Zero;
			BigDecimal actual = new BigDecimal(0, 1);

			Assert.True(BigDecimal.Equals(expected, actual));
			//Assert.AreEqual("0", actual.ToString());
		}

		[Test]
		public static void TestConstructor001D()
		{
			string? val1 = TestBigDecimalHelper.PrepareValue("0.5", _format);
			string? val2 = TestBigDecimalHelper.PrepareValue("0.5", _format);

			BigDecimal i = BigDecimal.Parse(val1);
			BigDecimal j = BigDecimal.Parse(val2);

			Assert.AreEqual(val1, i.ToString());
			Assert.AreEqual(val2, j.ToString());
		}

		[Test]
		public static void TestConstructor_Float()
		{
			string expected1 = "0.3486328";
			float d1 = 0.3486328125f;
			TestContext.WriteLine($"R: \"{expected1}\" decimal: {d1.ToString("R")}");
			TestContext.WriteLine($"E: \"{expected1}\" decimal: {d1.ToString("E")}");
			TestContext.WriteLine($"G9: \"{expected1}\" decimal: {d1.ToString("G9")}");
			TestContext.WriteLine($"G10: \"{expected1}\" decimal: {d1.ToString("G10")}");
			TestContext.WriteLine($"F9: \"{expected1}\" decimal: {d1.ToString("F9")}");
			BigDecimal actual1 = new BigDecimal(d1);

			Assert.AreEqual(expected1, actual1.ToString());
			TestContext.WriteLine($"{expected1} == {actual1}");
		}

		[Test]
		public static void TestConstructor_Double()
		{
			//TestConstructor_Double("0", 0);
			//TestConstructor_Double("0", 0.0);
			////TestConstructor_Double("0", -0.0);
			//
			//TestConstructor_Double("7976931348623157", 7976931348623157);
			//TestConstructor_Double("-7976931348623157", -7976931348623157);
			//
			//TestConstructor_Double("1000000000000000", 1000000000000000);
			//TestConstructor_Double("-1000000000000000", -1000000000000000);

			TestConstructor_Double("1.7976931348623157", 1.7976931348623157);
			TestConstructor_Double("-1.7976931348623157", -1.7976931348623157);

			TestConstructor_Double("0.0000000008623157", 0.0000000008623157);
			TestConstructor_Double("-0.0000000000623157", -0.0000000000623157);

			TestConstructor_Double("0.0101010101010101", 0.0101010101010101);
			TestConstructor_Double("-0.0101010101010101", -0.0101010101010101);
		}

		private static void TestConstructor_Double(string expected, Double value)
		{
			BigDecimal actual = new BigDecimal(value);
			Assert.AreEqual(expected, actual.ToString());
			TestContext.WriteLine($"{expected} == {actual}");
		}

		[Test]
		public static void TestConstructor001WriteLineA()
		{
			string? expected1 = TestBigDecimalHelper.PrepareValue("3.141592793238462", _format);
			string? expected2 = TestBigDecimalHelper.PrepareValue("3.141592793238462", _format);
			BigDecimal π = (BigDecimal)3.141592793238462m;
			BigDecimal d = new BigDecimal(BigInteger.Parse("3141592793238462"), -15);
			string? actual1 = π.ToString();
			string? actual2 = d.ToString();

			TestContext.WriteLine("π = " + actual1);
			TestContext.WriteLine("d = " + actual2);
			Assert.AreEqual(expected1, actual1);
			Assert.AreEqual(expected2, actual2);
		}

		[Test]
		public static void TestCastingDecimal()
		{
			Decimal m = 0.0000000000000001m;

			BigDecimal e = new BigDecimal(1000000000, -25);
			BigDecimal h = (BigDecimal)m;

			TestContext.WriteLine("m = " + m);
			TestContext.WriteLine("e = " + e);
			TestContext.WriteLine("h = " + h);

			Assert.AreEqual(h.ToString(), e.ToString());
		}

		[Test]
		public static void TestCastingDouble()
		{
			Double m = 0.0000000000000001d;

			BigDecimal e = new BigDecimal(1000000000, -25);
			BigDecimal h = (BigDecimal)m;

			TestContext.WriteLine("m = " + m);
			TestContext.WriteLine("e = " + e);
			TestContext.WriteLine("h = " + h);

			Assert.AreEqual(h.ToString(), e.ToString());
		}

		[Test]
		public static void TestConstructor002()
		{
			BigDecimal f = new BigDecimal(-3, -2);
			string? expected = TestBigDecimalHelper.PrepareValue("-0.03", _format);
			Assert.AreEqual(expected, f.ToString());
		}

		[Test]
		public static void TestConstructor003()
		{
			BigDecimal g = new BigDecimal(0, -1);
			Assert.AreEqual("0", g.ToString());
		}

		[Test]
		public static void TestConstructor004()
		{
			string? value = TestBigDecimalHelper.PrepareValue("-0.0012345", _format);

			BigDecimal h = BigDecimal.Parse(value);
			Assert.AreEqual(value, h.ToString());
		}

		[Test]
		public static void TestConstructorToString123456789()
		{
			const Int32 numbers = 123456789;
			string? expected = numbers.ToString();
			string? actual = new BigDecimal(numbers).ToString();

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestNormalizeB()
		{
			string? expected = "1000000";
			BigDecimal bigDecimal = new BigDecimal(1000000000, -3);

			string? actual = bigDecimal.ToString();
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParse001()
		{
			string expected = TestBigDecimalHelper.PrepareValue("0.00501", _format);
			BigDecimal result = BigDecimal.Parse(expected);
			string? actual = result.ToString();

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParseEmptyString()
		{
			BigDecimal result = BigDecimal.Parse("");
			bool condition = result == BigDecimal.Zero;
			
			Assert.True(condition);
		}
		
		[Test]
		public static void TestParseString0()
		{

			BigDecimal result = BigDecimal.Parse("0");
			Assert.True( result == BigDecimal.Zero );

		}
		[Test]
		public static void TestParseStringNegative0()
		{
		

			BigDecimal result = BigDecimal.Parse("-0");
			Assert.True(result == BigDecimal.Zero );
		}
		[Test]
		public static void TestParse002()
		{
			BigDecimal result1 = BigDecimal.Parse("");
			Assert.True(result1 == BigDecimal.Zero);

			BigDecimal result2 = BigDecimal.Parse("0");
			Assert.True( result2 == BigDecimal.Zero );

			BigDecimal result3 = BigDecimal.Parse("-0");
			Assert.True(result3 == BigDecimal.Zero );
		}

		[Test]
		public static void TestParse0031()
		{
			string expected = "-123456789";
			string? actual = BigDecimal.Parse(expected).ToString();

			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParse0032()
		{
			string expected = "123456789";
			BigDecimal bigDecimal = BigDecimal.Parse(expected);
			string? actual = bigDecimal.ToString();
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParse0033()
		{
			string expected = TestBigDecimalHelper.PrepareValue("1234.56789", _format);
			string? actual = BigDecimal.Parse(expected).ToString();
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParseScientific()
		{
			string toParse = TestBigDecimalHelper.PrepareValue("123.123E-20", _format);
			string expected = TestBigDecimalHelper.PrepareValue("0.00000000000000000123123", _format);
			BigDecimal parsed = BigDecimal.Parse(toParse);
			string actual = parsed.ToString();
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParseNegativeScientific()
		{
			string toParse = TestBigDecimalHelper.PrepareValue("-123.123E-20", _format);
			string expected = TestBigDecimalHelper.PrepareValue("-0.00000000000000000123123", _format);
			BigDecimal parsed = BigDecimal.Parse(toParse);
			string actual = parsed.ToString();
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		public static void TestParseEpsilon()
		{
			string expected = TestBigDecimalHelper.PrepareValue("4.9406564584124654E-324", _format);
			BigDecimal actual = BigDecimal.Parse(expected);

			Assert.AreEqual(expected, BigDecimal.ToScientificENotation(actual));
		}

		[Test]
		public static void TestParseLarge()
		{
			string expected = TestBigDecimalHelper.PrepareValue("4.9406564584124654E+324", _format);
			BigDecimal actual = BigDecimal.Parse(expected);

			Assert.AreEqual(expected, BigDecimal.ToScientificENotation(actual));
		}


		[Test]
		public static void TestParseRoundTrip()
		{
			double dval = 0.6822871999174d;
			string? val = TestBigDecimalHelper.PrepareValue("0.6822871999174", _format);
			BigDecimal actual = BigDecimal.Parse(val);
			BigDecimal expected = (BigDecimal)dval;
			
			Assert.True(BigDecimal.Equals(expected, actual));
		}

		[Test]
		[NonParallelizable]
		public static void TestAlwaysTruncate()
		{
			int savePrecision = BigDecimal.Precision;
			string? expected1 = TestBigDecimalHelper.PrepareValue("3.1415926535", _format);
			string? expected2 = TestBigDecimalHelper.PrepareValue("-3.1415926535", _format);
			string? expected3 = TestBigDecimalHelper.PrepareValue("-0.0000031415", _format);
			string? expected4 = "-3";

			string? actual1 = "";
			string? actual2 = "";
			string? actual3 = "";
			string? actual4 = "";

			try
			{
				BigDecimal.Precision = 10;
				BigDecimal.AlwaysTruncate = true;

				string? val1 = TestBigDecimalHelper.PrepareValue("3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535", _format);
				BigDecimal parsed1 = BigDecimal.Parse(val1);

				string? val2 = TestBigDecimalHelper.PrepareValue("-3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535", _format);
				BigDecimal parsed2 = BigDecimal.Parse(val2);

				string? val3 = TestBigDecimalHelper.PrepareValue("-0.00000314159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214808651328230664709384460955058223172535", _format);
				BigDecimal parsed3 = BigDecimal.Parse(val3);

				BigDecimal parsed4 = BigDecimal.Parse("-3");

				actual1 = parsed1.ToString();
				actual2 = parsed2.ToString();
				actual3 = parsed3.ToString();
				actual4 = parsed4.ToString();
			}
			finally
			{
				BigDecimal.Precision = savePrecision;
				BigDecimal.AlwaysTruncate = false;
			}

			Assert.AreEqual(expected1, actual1, "#: 1");
			Assert.AreEqual(expected2, actual2, "#: 2");
			Assert.AreEqual(expected3, actual3, "#: 3");
			Assert.AreEqual(expected4, actual4, "#: 4");
		}

		[Test]
		[NonParallelizable]
		public static void TestTruncateOnAllArithmeticOperations()
		{
			int savePrecision = BigDecimal.Precision;

			BigDecimal mod1 = BigDecimal.Parse("3141592653589793238462643383279502");
			BigDecimal mod2 = BigDecimal.Parse("27182818284590452");
			//BigDecimal neg1 = BigDecimal.Parse("-3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647");

			string? val1 = TestBigDecimalHelper.PrepareValue("3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647", _format);
			BigDecimal lrg1 = BigDecimal.Parse(val1);

			string? val2 = TestBigDecimalHelper.PrepareValue("2.718281828459045235360287471352662497757247093699959574966967", _format);
			BigDecimal lrg2 = BigDecimal.Parse(val2);

			string? expected1 = TestBigDecimalHelper.PrepareValue("5.859874482", _format);
			string? expected2 = TestBigDecimalHelper.PrepareValue("0.423310825", _format);
			string? expected3 = TestBigDecimalHelper.PrepareValue("8.539734222", _format);
			string? expected4 = TestBigDecimalHelper.PrepareValue("0.865255979", _format);
			string? expected5 = TestBigDecimalHelper.PrepareValue("9.869604401", _format);
			string? expected6 = TestBigDecimalHelper.PrepareValue("148.4131591", _format);
			string? expected7 = "80030773195";
			string? expected8 = TestBigDecimalHelper.PrepareValue("-3.14159265", _format);
			string? expected9 = "3";
			string? expected10 = "4";
			string? expected11 = TestBigDecimalHelper.PrepareValue("3.141592653", _format);

			string? actual1 = "";
			string? actual2 = "";
			string? actual3 = "";
			string? actual4 = "";
			string? actual5 = "";
			string? actual6 = "";
			string? actual7 = "";
			string? actual8 = "";
			string? actual9 = "";
			string? actual10 = "";
			string? actual11 = "";

			try
			{
				BigDecimal.Precision = 50;
				BigDecimal.AlwaysTruncate = false;

				TestContext.WriteLine($"E = {BigDecimal.E}");
				TestContext.WriteLine($"{new BigDecimal(lrg1.Mantissa, lrg1.Exponent)}");
				TestContext.WriteLine($"{new BigDecimal(lrg2.Mantissa, lrg2.Exponent)}");

				BigDecimal result1 = BigDecimal.Add(lrg1, lrg2);
				BigDecimal result2 = BigDecimal.Subtract(lrg1, lrg2);
				BigDecimal result3 = BigDecimal.Multiply(lrg1, lrg2);
				BigDecimal result4 = BigDecimal.Divide(lrg2, lrg1);
				BigDecimal result5 = BigDecimal.Pow(lrg1, 2);
				BigDecimal result6 = BigDecimal.Exp(new BigInteger(5));
				BigDecimal result7 = BigDecimal.Mod(mod1, mod2);
				BigDecimal result8 = BigDecimal.Negate(lrg1);
				BigDecimal result9 = BigDecimal.Floor(lrg1);
				BigDecimal result10 = BigDecimal.Ceiling(lrg1);
				BigDecimal result11 = BigDecimal.Abs(lrg1);

				actual1 = new string(result1.ToString().Take(11).ToArray());
				actual2 = new string(result2.ToString().Take(11).ToArray());
				actual3 = new string(result3.ToString().Take(11).ToArray());
				actual4 = new string(result4.ToString().Take(11).ToArray());
				actual5 = new string(result5.ToString().Take(11).ToArray());
				actual6 = new string(result6.ToString().Take(11).ToArray());
				actual7 = new string(result7.ToString().Take(11).ToArray());
				actual8 = new string(result8.ToString().Take(11).ToArray());
				actual9 = new string(result9.ToString().Take(11).ToArray());
				actual10 = new string(result10.ToString().Take(11).ToArray());
				actual11 = new string(result11.ToString().Take(11).ToArray());
			}
			finally
			{
				BigDecimal.Precision = savePrecision;
				BigDecimal.AlwaysTruncate = false;
			}

			Assert.AreEqual(expected1, actual1, $"Test Truncate On All Arithmetic Operations  - #1: ");
			Assert.AreEqual(expected2, actual2, $"Test Truncate On All Arithmetic Operations  - #2: ");
			Assert.AreEqual(expected3, actual3, $"Test Truncate On All Arithmetic Operations  - #3: ");
			Assert.AreEqual(expected4, actual4, $"Test Truncate On All Arithmetic Operations  - #4: ");
			Assert.AreEqual(expected5, actual5, $"Test Truncate On All Arithmetic Operations  - #5: ");
			StringAssert.StartsWith(expected6, actual6, $"Test Truncate On All Arithmetic Operations  - #6: ");
			Assert.AreEqual(expected7, actual7, $"Test Truncate On All Arithmetic Operations  - #7: ");
			Assert.AreEqual(expected8, actual8, $"Test Truncate On All Arithmetic Operations  - #8: ");
			Assert.AreEqual(expected9, actual9, $"Test Truncate On All Arithmetic Operations  - #9: ");
			Assert.AreEqual(expected10, actual10, $"Test Truncate On All Arithmetic Operations - #10: ");
			Assert.AreEqual(expected11, actual11, $"Test Truncate On All Arithmetic Operations - #11: ");						
		}
	}

}