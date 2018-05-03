using Castle.Components.DictionaryAdapter.Xml;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Personify.Helpers;
using SeleniumFramework.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Threading;


namespace SeleniumFramework.PageClasses
{
    public class PageBase
    {
        internal IWebDriver Driver;
        internal PageClassFactory Pages;


        #region Common Page Methods

        internal IWebElement WaitForElementVisible(By locator)
        {
            var ele = WaitHelpers.WaitForElementVisible(locator);
            //ToDo: Hack for network lag, must be removed. - <Pramod and 06/03/2017>
           
            NetLagWait();
            return ele;
        }
        
        internal IWebElement WaitForElementExist(By locator)
        {
            var ele = WaitHelpers.WaitForElementExist(locator);
            //ToDo: Hack for network lag, must be removed. - <Pramod and 06/03/2017>
          
            NetLagWait();
            return ele;
        }
                
        internal IWebElement WaitForClickable(By by)
        {
            var ele = WaitHelpers.WaitForClickable(by);
            //ToDo: Hack for network lag, must be removed. - < Pramod and 06 / 03 / 2017 >
            NetLagWait();

            return ele;
        }

        internal IWebElement WaitForClickable(IWebElement ele)
        {
            WaitHelpers.WaitForClickable(ele);
            //ToDo: Hack for network lag, must be removed. - < Pramod and 06 / 03 / 2017 >
            
            NetLagWait();

            return ele;
        }
        public void WaitForAjaxLoad()
        {
            WaitHelpers.WaitForAjaxLoad();
            //ToDo: Hack for network lag, must be removed. - <Pramod and 06/03/2017>
            NetLagWait();
        }
        
        internal void ScrollWindowToTop()
        {
            Executor.ExecuteJavascript("window.scrollBy(0,-1000)");
            ExplicitWait();
        }

        internal void SetImplicitWait(TimeSpan ts)
        {
            WebDriverHelpers.SetImplicitWait(ts);
        }

        internal void ScrollWindowToBottom()
        {
            Executor.ExecuteJavascript("window.scrollBy(0,1000)");
            ExplicitWait();
        }

        internal void ScrollWindowToMedium()
        {
            Executor.ExecuteJavascript("window.scrollBy(0,500)");
            ExplicitWait();
        }

        internal void ScrollIntoView(IWebElement ele)
        {
            ele.ScrollIntoView();
        }

        internal string AssertErrorMessageString(IWebElement element, string expected, string actual, string optionalMessage = null, string controlNameOverride = null)
        {
            string elementIdentifier;

            if (string.IsNullOrEmpty(controlNameOverride))
            {
                if (string.IsNullOrEmpty(element.GetAttribute("Id"))) elementIdentifier = element.GetAttribute("Name");

                else
                {
                    elementIdentifier = element.GetAttribute("Id");
                }
            }

            else
            {
                elementIdentifier = controlNameOverride;
            }
            if (string.IsNullOrEmpty(elementIdentifier)) elementIdentifier = "Element";

            return $"::Test Failed:: {elementIdentifier} had an expected value of: \"{expected}\" the actual value was: \"{actual}\".\r\n::{optionalMessage}";
        }

        public string HandleJavaAlert(bool dismiss = false)
        {
            //TODO: Hack, added by Mark 3/13, please remove
            NetLagWait();
            return WebDriverHelpers.HandleJavaAlert(dismiss);
        }

        public void ChangeTab(string tab, string dropDownItem = null)
        {
            WebElementHelpers.ChangeTab(tab,dropDownItem);
        }

        public void WaitForPresent(IWebElement element)
        {
            element.WaitForPresent(Driver);
        }
                
        public void NetLagWait()
        {
            Thread.Sleep(Config.NetLagWait);
        }

        public void ExplicitWait(int timeout = 500)
        {
            WaitHelpers.ExplicitWait(timeout);
        }
        
        #endregion
    }
}