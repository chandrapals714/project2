using OpenQA.Selenium;
using SeleniumFramework.PageClasses;



namespace SeleniumFramework.Helpers
{
    public class PageClassFactory
    {
        private readonly IWebDriver _driver;
        private LogInPage _logIn;
        private Workflows _workflows;
      

        public PageClassFactory(IWebDriver driver)
        {
            _driver = driver;
          
        }

     
        public LogInPage LogIn()
        {
            if (_logIn == null)
            {
                return _logIn = new LogInPage(_driver);
            }
            return _logIn;
        }
        
        public Workflows Workflows()
        {
            if (_workflows == null)
            {
                return _workflows = new Workflows(_driver);
            }
            return _workflows;
        }



    } //end of PageClassFactory class

       
}// end of namespace
