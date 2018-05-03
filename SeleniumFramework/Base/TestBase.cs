using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using Personify.Helpers;
using SeleniumFramework.Generators;
using SimpleLogger;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeleniumFramework.Helpers
{
    [TestClass]
    public abstract class TestBase
    {
        public IWebDriver Driver;
        public PageClassFactory Pages;
        public UserGenerator UserGenerator;
        public string ModuleName = "CRM360";
        private static IEnumerable<int> _pidsBefore;
        public string CurrentDirectory;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestSetup()
        {
            Config.Load();
            if (Config.LoggingOn)
            {
                Logger.LoggerHandlerManager.AddHandler(new FileLoggerHandler($"C:\\AutomationFiles\\Logs\\{TestContext.TestName}.log"));
                Logger.On();
            }

            Logger.Log(":");

            _pidsBefore = Process.GetProcessesByName(Config.Browser.ToLower()).Select(p => p.Id);

            CheckForTestRunOverides();

            UserGenerator = new UserGenerator();

            SetUpWebDriver();

            CurrentDirectory = GetCurrentDir();

            Pages = new PageClassFactory(Driver);

            Driver.Manage().Window.Maximize();

            Pages.LogIn().NavigateTo(ModuleName);

            if (Config.Browser != "Mobile")
            {
                WebDriverFactory.CurrentBrowserPID = Process.GetProcessesByName(Config.Browser.ToLower()).Select(p => p.Id).Except(_pidsBefore).ToArray()[0];
            }

            if (Config.Browser == "Mobile" && false)
            {
                var params1 = new Dictionary<string, string>();
                params1.Add("wifi", "disabled");

                var res = Executor.ExecuteJavascript("mobile:network.settings:set", params1);

                params1.Add("wifi", "enabled");
                res = Executor.ExecuteJavascript("mobile:network.settings:set", params1);
            }

            WebDriverHelpers.SetImplicitWait(Config.DefaultTimeSpan);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            
            Logger.Log(":");
            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed && Config.IsTestAgent)
            {
                var pic = Driver.TakeScreenshot();

                var filePath = Path.Combine(TestContext.ResultsDirectory,
                    $"{TestContext.TestName.Split('_')[0]}.jpg");

                pic.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
                TestContext.AddResultFile(filePath);
                //TestContext.Properties.Add("ScreenShotLoc", Path.Combine(TestContext.ResultsDirectory, "FailureSnapshot.jpg"));
            }

            Driver.Close();
            Driver.Quit();

        }

        public string FailedAssertionMessage(string assertionThatFailed, string expected, string actual, string optionalMessage = null)
        {
            return $"::Test Failed::{assertionThatFailed} expected result: {expected} actual result: {actual}:: {optionalMessage}";
        }

        private string GetCurrentDir()
        {
            return Regex.Split(Directory.GetCurrentDirectory(), "bin")[0];
        }

        private void SetUpWebDriver()
        {
            Logger.Log(":");

            switch (Config.Browser)
            {
                case "FireFox":
                    Driver = WebDriverFactory.GetDriver(BrowserType.FireFox);
                    break;
                case "IExplore":
                    Driver = WebDriverFactory.GetDriver(BrowserType.IExplore);
                    break;
                case "Chrome":
                    Driver = WebDriverFactory.GetDriver(BrowserType.Chrome);
                    break;
                case "Edge":
                    Driver = WebDriverFactory.GetDriver(BrowserType.Edge);
                    break;
                case "Mobile":
                    DesiredCapabilities capabilities =null;
                    //capabilities.SetCapability("platformName", "iOS");
                    //capabilities.SetCapability("platformVersion", "10.1.1");
                    //capabilities.SetCapability("manufacturer", "Apple");
                    //capabilities.SetCapability("model", "iPad Pro");
                    //capabilities.SetCapability("location", "NA-US-BOS");
                    //capabilities.SetCapability("resolution", "2732 x 2048");

                    Driver = WebDriverFactory.GetDriver(BrowserType.Mobile, Config.PerfectoUserName, Config.PerfectoPassword,
                        Config.MobileDevices,null, capabilities);
                    break;
                default:
                    Driver = WebDriverFactory.GetDriver(BrowserType.FireFox);
                    break;
            }
        }

        private void CheckForTestRunOverides()
        {
            Logger.Log(":");
            try
            {
                if (!String.IsNullOrEmpty(TestContext.Properties["Browser"].ToString()))
                {
                    Config.Browser = TestContext.Properties["Browser"].ToString();
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (!String.IsNullOrEmpty(TestContext.Properties["WebUrl"].ToString()))
                {
                    Config.WebUrl = TestContext.Properties["WebUrl"].ToString();
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (!String.IsNullOrEmpty(TestContext.Properties["ConnectionString"].ToString()))
                {
                    Config.ConnectionString = TestContext.Properties["ConnectionString"].ToString();
                }
            }
            catch (Exception)
            {

            }

            try
            {
                if (!String.IsNullOrEmpty(TestContext.Properties["IsTestAgent"].ToString()))
                {
                    Config.IsTestAgent = Convert.ToBoolean(TestContext.Properties["IsTestAgent"].ToString());
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
