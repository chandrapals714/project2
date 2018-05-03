using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumFramework;
using SeleniumFramework.Helpers;
using System;

namespace Web.UiTests.FullRegression.Tests
{

    [TestClass]
    public class SandBox : TestBase
    {
       
        [TestMethod]
        [TestCategory("Zz_Sandbox Temp Testing")]
        public void SandBoxTests()
        {

            //Perform the Test Scenario Operations
            //Login to Application
            Pages.Workflows().LogIn(Config.UserName, Config.Password);



        }
    }

}