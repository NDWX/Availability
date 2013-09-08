using Pug.Availability;
using Pug.Availability.Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pug.Availability.Tests
{
    
    
    /// <summary>
    ///This is a test class for HttpCheckerHttpCheckerTests and is intended
    ///to contain all HttpCheckerHttpCheckerTests Unit Tests
    ///</summary>
	[TestClass()]
	public class HttpCheckerHttpCheckerTests
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for Check
		///</summary>
		[TestMethod()]
		public void CheckTestHttpChecker()
		{
			string host = "http://www.ezysend.com"; // TODO: Initialize to an appropriate value
			int port = 80; // TODO: Initialize to an appropriate value
			HttpChecker target = new HttpChecker(new Uri(host)); // TODO: Initialize to an appropriate value
			CheckResult expected = null; // TODO: Initialize to an appropriate value
			CheckResult actual;
			actual = target.Check();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
