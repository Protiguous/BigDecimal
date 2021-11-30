﻿// Copyright © Protiguous. All Rights Reserved.
// 
// This entire copyright notice and license must be retained and must be kept visible in any binaries, libraries, repositories, or source code (directly or derived) from our binaries, libraries, projects, solutions, or applications.
// 
// All source code belongs to Protiguous@Protiguous.com unless otherwise specified or the original license has been overwritten by formatting. (We try to avoid it from happening, but it does accidentally happen.)
// 
// Any unmodified portions of source code gleaned from other sources still retain their original license and our thanks goes to those Authors.
// If you find your code unattributed in this source code, please let us know so we can properly attribute you and include the proper license and/or copyright(s).
// If you want to use any of our code in a commercial project, you must contact Protiguous@Protiguous.com for permission, license, and a quote.
// 
// Donations, payments, and royalties are accepted via bitcoin: 1Mad8TxTqxKnMiHuZxArFvX8BuFEB9nqX2 and PayPal: Protiguous@Protiguous.com
// 
// ====================================================================
// Disclaimer:  Usage of the source code or binaries is AS-IS.
// No warranties are expressed, implied, or given.
// We are NOT responsible for Anything You Do With Our Code.
// We are NOT responsible for Anything You Do With Our Executables.
// We are NOT responsible for Anything You Do With Your Computer.
// ====================================================================
// 
// Contact us by email if you have any questions, helpful criticism, or if you would like to use our code in your project(s).
// For business inquiries, please contact me at Protiguous@Protiguous.com.
// Our software can be found at "https://Protiguous.com/Software/"
// Our GitHub address is "https://github.com/Protiguous".
// 
// File "TestBigDecimalCritical.cs" last formatted on 2021-10-03 at 4:55 AM by Protiguous.

namespace TestBigDecimal {

	using System;
	using ExtendedNumerics;
	using FluentAssertions;
	using NUnit.Framework;

	[Parallelizable( ParallelScope.All )]
	[TestFixture]
	public class TestBigDecimalCritical {

		[Test]
		public void Test47() {
			var π1 = 1 * BigDecimal.π;
			var π2 = 2 * BigDecimal.π;
			var π4 = 4 * BigDecimal.π;
			var π8 = 8 * BigDecimal.π;
			var sum = π1 + π2 + π4 + π8;
			var t = ( Int32 ) sum.WholeValue;

			Assert.AreEqual( 47, t );
		}

		[Test]
		public void TestConstructor0() {
			BigDecimal expected = 0;
			var actual = new BigDecimal( 0 );

			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestConstructor00() {
			BigDecimal expected = 0;
			var actual = new BigDecimal( 0, 0 );

			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestConstructor001D() {
			var i = BigDecimal.Parse( "0.5" );
			var j = BigDecimal.Parse( "0.01" );

			Assert.AreEqual( "0.5", i.ToString() );
			Assert.AreEqual( "0.01", j.ToString() );
		}

		[Test]
		public void TestConstructor001WriteLineA() {
			var π = ( BigDecimal ) 3.141592793238462m;
			var d = new BigDecimal( 3141592793238462, -15 );
			TestContext.WriteLine( "π = " + π );
			TestContext.WriteLine( "d = " + d );

			Assert.AreEqual( π, d );
		}

		[Test]
		public void TestConstructor001WriteLineB() {
			const Decimal m = 0.0000000000000001m;

			var e = new BigDecimal( 1000000000, -25 );
			var h = ( BigDecimal ) m;

			TestContext.WriteLine( "m = " + m );
			TestContext.WriteLine( "e = " + e );
			TestContext.WriteLine( "h = " + h );

			Assert.AreEqual( h.ToString(), e.ToString() );
		}

		[Test]
		public void TestConstructor002() {
			var f = new BigDecimal( -3, -2 );
			Assert.AreEqual( "-0.03", f.ToString() );
		}

		[Test]
		public void TestConstructor003() {
			var g = new BigDecimal( 0, -1 );
			Assert.AreEqual( "0", g.ToString() );
		}

		[Test]
		public void TestConstructor004() {
			var h = BigDecimal.Parse( "-0.0012345" );
			Assert.AreEqual( "-0.0012345", h.ToString() );
		}

		[Test]
		public void TestConstructorToString123456789() {
			const Int32 numbers = 123456789;
			var expected = numbers.ToString();
			var actual = new BigDecimal( numbers ).ToString();

			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestNormalizeB() {
			var expected = BigDecimal.Parse( "1000000" );
			var actual = new BigDecimal( 1000000000, -3 );

			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestParse001() {
			const String expected = "0.00501";
			var result = BigDecimal.Parse( expected );
			var actual = result.ToString();

			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestParse002() {
			var result1 = BigDecimal.Parse( "" );
			Assert.AreEqual( result1, BigDecimal.Zero );

			var result2 = BigDecimal.Parse( "0" );
			Assert.AreEqual( result2, BigDecimal.Zero );

			var result3 = BigDecimal.Parse( "-0" );
			Assert.AreEqual( result3, BigDecimal.Zero );
		}

		[Test]
		public void TestParse0031() {
			const String expected = "-123456789";
			var bigDecimal = BigDecimal.Parse( expected );
			Decimal.Parse( expected ).Should()?.Be( ( Decimal ) bigDecimal );
			var actual = bigDecimal.ToString();
			Assert.AreEqual( expected, actual );
		}

		[Theory]
		[Test]
		public void TestParse0032() {
			const String expected = "123456789";
			var bigDecimal = BigDecimal.Parse( expected );
			Decimal.Parse( expected ).Should()?.Be( ( Decimal ) bigDecimal );
			var actual = bigDecimal.ToString();
			Assert.AreEqual( expected, actual );
		}

		[Test]
		public void TestParse0033() {
			const String expected3 = "1234.56789";

			var result3 = BigDecimal.Parse( expected3 );

			var dec3 = Decimal.Parse( expected3 );
			dec3.Should()?.Be( ( Decimal ) result3 );

			var actual3 = result3.ToString();
			Assert.AreEqual( expected3, actual3 );
		}

		[Test]
		public void TestParse004() {
			var result1 = ( Decimal ) BigDecimal.Parse( "0.125" );
			var dec1 = Decimal.Parse( "0.125" );
			dec1.Should()?.Be( result1 );

			var result2 = ( Decimal ) BigDecimal.Parse( "-0.0625" );
			var dec2 = Decimal.Parse( "-0.0625" );
			dec2.Should()?.Be( result2 );

			//result1.Should().BeAssignableTo<BigDecimal>( "Tried to parse: '0.125'" );
			//result2.Should().BeAssignableTo<BigDecimal>( "Tried to parse: '-0.0625'" );
		}

		[Test]
		public void TestSubtractions(
			[Random( -8.98846567431158E+300D, 8.98846567431158E+300D, 10 )] Double b,
			[Random( -8.98846567431158E+300D, 8.98846567431158E+300D, 10 )] Double d
		) {
			var strB = $"{b:R}";
			var strD = $"{d:R}";

			TestContext.WriteLine( $"{b:R} = {strB}" );
			TestContext.WriteLine( $"{d:R} = {strD}" );

			var bigB = BigDecimal.Parse( strB );
			var bigD = BigDecimal.Parse( strD );

			TestContext.WriteLine( Environment.NewLine );
			TestContext.WriteLine( $"bigB = {bigB}" );
			TestContext.WriteLine( $"bigD = {bigD}" );

			var result1 = BigDecimal.Subtract( bigB, bigD );
			var result2 = bigB - bigD;

			result1.Should()?.Be( result2 );
		}

		[Test]
		public void TestTruncate() {
			const String inputTruncated =
				"38776413731534507341472294220970933835515664718260518542692164892369393388454765429965711304132864249950074173248631118139885711281403156400182208418498132380665348582256048635378814909035638369142648772177618951899185003568005598389270883746269077440991532220847404333505059368816860680653357748237545067181074698997348124273540082967040205337039719556204791774654186626822192852578297197622945023468587167940717672154626982847038945027431144177383552390076867181200131087852865589018597759807623800948540502708501473286123912110702619773244550322465985979980114779581215743799790210865866959716136152785422203785552850816565888483726280027736811701443283167094373785256268739306209472514414456698923382789454032363968616464876677737866600848986505927023714735496267888826964325695603484817243244809072199216323431074501911199589021095576091452848741385260278621154863084476153732935785975553768625991893151359011912157996422994871709298494233782632174324940552077998861058801035481527689611495569489001108047129667715138204276438217877458404549511313153150137527893798615996618488836664617057038126333313180040094667868763537391421370724952266262848120654117339947389714860375532634890853303061644123428823851175161042458377024247370039795707768407904842511280809838660155394320788005292339449327116941969519022963362256288633034183673183754543670394109495242494711094883817203892173617367313695468521390931623680173196403022932833410798171066411176995128779282872081932608267999112302172207922731784899893348412676098162467010668924433588685153611407005617772276741793479524433622378470125354022566116327819435029065557564148488746638319658731270457607183892425161850287824787546065413294231650473976473355046501500793677901782339691542233183741598103696583351788651177203938936918102755367072014549821942367309956671236707350861545099496206538228683951185018840006137763162355709495445928668480960580978979870379511703883251713690511544429859593313279574155504139941107166963497890833932811052504269372145803660626639428643564562691059910703703938694915154537936003382455188656514686359660013747580119285264755448830584594983111162605867224680013454700621697086948523549156403848856543212816956769085216390639154261614649538130954560421673680672884105498050605587531872704107707071402689983600332112655608194612408217782173036018661692139351433658340756975168361107372727516912020823362368253159826937134217107045868191298957690827125630453728790792408734840661702578638598543186544910552465999106381802375938701350575940262569041045146526024334627822715612658351899764042223444201035443823410277761971257862200600465373558428055133799307959576455801692979753194304758921759067399106319456847661528054181651013888120488047974670158855437555210689546049958555745098303660202886404313365902203237775035723926097742965028613593632230336269392684340085274710999024668887638930755250701806345477524832568256645103704878731032912768646402146422301881142289323523789305126831904241622042944333916620344863470012778933196413192781253025453531244850133026071231714118351386262249150472643870800725983523611903791303553632632769972142502483519860983067322477753824959399886980031912842700140970151007657989042261109130704991895244868527969247414974047405237324669264878742391500642753525622057641241164177505839173992651361990366480244195157062835803031557544691492841007028723179639729081951702197292799161437892952439082270465575308762112590993865133052593362045638622447863872110087219994330766670422412140283392118259566085972052360790645394540700438378734059789109046910356858343004387656915432928337709841252916626015752013241699464443045041876948902728601721842214670716585909801092203893128618468720651888522728597430373030188565238122801065278124235661294292641028550276301054915567825793810248724267437857461336921376742513529432313053995421425528496990787018582251366776291943999044323970133345610820834058391982655766601126736285624213085882525085728598384700565577250732861707158419417137322187913601105221450993534840307771350787020353312910993312574109077271828643659506792514058623896881407687463008239648545371730757776422468606770212802349464720937428992320859723193781752582479518699133569129895070994026410312951649922900688489934852155962598169718199217867461287501481443450777777718726084902136902441480397119384141792970385831367927360530354214563521320900336914169681532166791668676942898880184098720787172114194029069821244937592713815214434788393095503048740886117426353441330676447598548976011441527165748380891340472246800001389307364429687469295246232117792720007673578989468325170179570094184486525355114468774857635615955720968054081874458733938769018227387365842825259166694681823057556598910704367318366050815517174086712448729791746859581490981539042615521145996146036468018904747552880641671500884541141688250485043379587617571474356672696577799617797264760021142950373293666619603196041523466051054935554966409263947312788960948665394281916627352113060683470291466105925";
			const String inputOverflow = "919919200639429489197056";

			var expectedResult = BigDecimal.Parse( inputTruncated );
			var result = BigDecimal.Parse( String.Concat( inputTruncated, inputOverflow ) );
			result = BigDecimal.Truncate( result, 5000 );
			var success = expectedResult.Equals( result );

			Assert.IsTrue( success );
		}

	}

}