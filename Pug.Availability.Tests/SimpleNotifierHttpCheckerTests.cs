
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Pug.Availability;
using Pug.Availability.Notifiers.Sms.Inteltech;

namespace Pug.Availability.Tests
{
    
    
    /// <summary>
    ///This is a test class for SimpleNotifierHttpCheckerTests and is intended
    ///to contain all SimpleNotifierHttpCheckerTests Unit Tests
    ///</summary>
	[TestClass()]
	public class SimpleNotifierHttpCheckerTests
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
		///A test for Notify
		///</summary>
		[TestMethod()]
		public void NotifyTestHttpChecker()
		{
			string username = "Andrian"; // TODO: Initialize to an appropriate value
			string key = "f3aa9bf27db60674f228fd90beeb028897657c4a6450fc9de8a8c87221a12dcfe"; // TODO: Initialize to an appropriate value
			string senderName = "ND"; // TODO: Initialize to an appropriate value
			IEnumerable<string> destinations = new string[] {"+61433003505" }; // TODO: Initialize to an appropriate value
			SimpleNotifier target = new SimpleNotifier(username, key, senderName, destinations); // TODO: Initialize to an appropriate value
			CheckResult checkResult = new CheckResult(false, new Dictionary<string, string>()); // TODO: Initialize to an appropriate value
			string configuration = "Test"; // TODO: Initialize to an appropriate value
			target.Notify(checkResult, configuration);
			Assert.Inconclusive("A method that does not return a value cannot be verified.");
		}
	}
}
