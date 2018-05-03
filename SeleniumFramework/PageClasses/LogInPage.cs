using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumFramework.PageClasses;
using System.Text.RegularExpressions;
using Personify.Helpers;
using SimpleLogger;

namespace SeleniumFramework.PageClasses
{
    public class LogInPage : PageBase 
    {
        #region Page Elements
        
        private IWebElement _userIdEdit => Driver.FindElement(By.Name("UserName"));
        private IWebElement _passWordEdit => Driver.FindElement(By.Name("Password"));
        
        private IWebElement _logInButton => Driver.FindElement(By.CssSelector(".btn.btn-primary.btn-block"));
        
        private IWebElement _windowsAuthLogInButton => Driver.FindElement(By.LinkText("Login with Windows Authentication"));

        #endregion

        #region Constructor
        public LogInPage(IWebDriver driver)
        {
            Driver = driver;
        }
        #endregion

        #region Page Actions

        public void NavigateTo(string moduleName="CRM360")
        {
            //Driver.Navigate().GoToUrl($"{Config.WebUrl}/{moduleName}/Index");
            Driver.Navigate().GoToUrl($"{Config.WebUrl}");
            WaitForAjaxLoad();
        }

        public void SetTextUserEdit(string text)
        {
            WaitForClickable(_userIdEdit);
            _userIdEdit.SetTextStandardTextBox(text);
        }

        public void SetTextPasswordEdit(string text)
        {
            WaitForClickable(_passWordEdit);
            _passWordEdit.SetTextStandardTextBox(text);
        }

        public void ClickLogInButton()
        {
            WaitForClickable(_logInButton);
            _logInButton.Click();
        }

        public void ClickLogInWithWindowsAuthetication()
        {
            WaitForClickable(_windowsAuthLogInButton);
            _windowsAuthLogInButton.Click();
        }

        public void LogIn(string user, string password)
        {
            Logger.Log($"Logging in using:{user}/{password}");
            SetTextUserEdit(user);
            SetTextPasswordEdit(password);
            ClickLogInButton();
            if (Config.Browser == "Mobile")
            {
                HandleJavaAlert();
            }
            WaitHelpers.WaitForLoadingOverlays(Driver);
            ExplicitWait();
            WaitForAjaxLoad();
        }

        #endregion

        #region Helpers
        
        #endregion
    }

    class PageDidNotLoadException : Exception
    {
        public PageDidNotLoadException(string error)
        {
            throw  new Exception("Page failed to load properly, test did not execute. Reason: " + error);
        }
    }

}   