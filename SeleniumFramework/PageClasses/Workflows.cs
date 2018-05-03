using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumFramework.Generators.GeneratorData;
using SeleniumFramework.Helpers;
using Personify.Helpers;
using SeleniumFramework.Generators;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.Script;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.Extensions;
using SeleniumExtensions;
using SimpleLogger;
using By = Selenium.WebDriver.Extensions.By;

namespace SeleniumFramework.PageClasses
{

    public class Workflows : PageBase
    {
        private readonly IWebDriver _driver;
        private readonly PageClassFactory _pages;

        public Workflows(IWebDriver driver)
        {
            _driver = driver;
            _pages = new PageClassFactory(_driver);
        }

        private string LoadJS()
        {
            return "if(window.app){app.loading = []};" +
                   "console.log('loaded stuff');" +
                   "require.config({" +
                   "onNodeCreated: function (node, config, moduleName, url) {" +
                   "app.loading.push(moduleName);" +
                   "node.addEventListener('load', function () {" +
                   "app.loading = _.without(app.loading, moduleName);});" +
                   "node.addEventListener('error', function () {" +
                   "app.loading = _.without(app.loading, moduleName);});}});";
        }

       
        public void LogIn(string user, string password)
        {
             //WebDriverFactory.LastDriver.Navigate().Refresh();
            _pages.LogIn().LogIn(user, password);
            //_pages.CRM().WaitForCreateNewButton();
            //_pages.CRM().WaitForPersonifyLogo(); //Modifying this for Orders, Id for 'Create New' button is not same for Order module.

            //TODO: Hack for network lag, must be removed. - <Pramod and 06/03/2017>
            NetLagWait();
            if (Config.Browser == "IExplore")
            {
                ExplicitWait(1000);
            }
            Executor.ExecuteJavascript(LoadJS()); //There must be no comments in the js files.


        }
        
       

    }//end of class

}//end of namespace