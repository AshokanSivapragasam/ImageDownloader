using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTimesheets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program prgm = new Program();
            prgm.Setup();
            prgm.SeleniumTestFormExecution();
            prgm.Teardown();
        }

        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        
        public void Setup()
        {
            driver = new InternetExplorerDriver();
            baseURL = "http://www.seleniummaster.com/seleniumformtest/registrationform.aspx";
            verificationErrors = new StringBuilder();
        }
        
        public void SeleniumTestFormExecution()
        {
            driver.Navigate().GoToUrl(baseURL + "/seleniumformtest/registrationform.aspx");
            driver.FindElement(By.Id("firstNameTextBox")).Clear();
            driver.FindElement(By.Id("firstNameTextBox")).SendKeys("abcd");
            driver.FindElement(By.Id("lastNameTextBox")).Clear();
            driver.FindElement(By.Id("lastNameTextBox")).SendKeys("abcd");
            driver.FindElement(By.Id("emailTextBox")).Clear();
            driver.FindElement(By.Id("emailTextBox")).SendKeys(" abcd@abcd.com");
            driver.FindElement(By.Id("phoneTextBox")).Clear();
            driver.FindElement(By.Id("phoneTextBox")).SendKeys("555-123-4567");
            new SelectElement(driver.FindElement(By.Id("booksDropDownList"))).SelectByText("Selenium RC");
            driver.FindElement(By.Id("osRadioButtonList_2")).Click();
            driver.FindElement(By.Id("registerButton")).Click();
        }
        
        public void Teardown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
