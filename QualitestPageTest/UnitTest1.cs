using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using QualitestPage;
using System.Collections.Generic;
using System.Web;

[assembly: Parallelizable(ParallelScope.All)]

namespace QualitestPageTest
{
    [TestFixture]
    public class QualitestPageTest
    {
        List<User> usersList;
        string NavigationPage = "https://qualitestgroup.com/contact-us/";

        [OneTimeSetUp]
        public void Initialize() 
        {
            usersList = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("DataFile.json"));  
        }

        [Test, Category("Negative Test - Empty Fields")]  
        public void SendRequestWithEmptyField_SentFaild_ReturnsTrue()
        {
            IWebDriver driver = new ChromeDriver();
            SelectElement selectElement;

            NavigateToContactUsPage(driver);

            foreach (User a in usersList)
            {
                driver.FindElement(By.Id("firstname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Clear();
                driver.FindElement(By.Id("firstname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(a.FirstName);

                driver.FindElement(By.Id("lastname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Clear();
                driver.FindElement(By.Id("lastname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(a.LastName);

                driver.FindElement(By.Id("company-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Clear();
                driver.FindElement(By.Id("company-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(a.CompanyName);

                driver.FindElement(By.Id("what_would_you_like_to_talk_about_0-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Click();

                driver.FindElement(By.Id("phone-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Clear();
                driver.FindElement(By.Id("phone-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(a.PhoneNumber);

                selectElement = new SelectElement(driver.FindElement(By.Id("location-34dd68e0-b077-4e95-9243-b861f3f2fd7d")));          
                if(!(a.Location.Length == 0))selectElement.SelectByText(a.Location);              

                driver.FindElement(By.Id("email-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Clear();
                driver.FindElement(By.Id("email-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(a.Email);
                driver.FindElement(By.CssSelector("#hsForm_34dd68e0-b077-4e95-9243-b861f3f2fd7d > div > div.actions > input")).Click();

                if (!(driver.FindElement(By.CssSelector("#hsForm_34dd68e0-b077-4e95-9243-b861f3f2fd7d > div > div.actions > input")).Displayed))
                {
                    CleanUp(driver);
                    Assert.IsTrue(false); 
                }
            }
            Assert.IsTrue(true);
            CleanUp(driver);
        }

        [Test, Category("Negative Test - Invalid Inputs")]
        public void InValidPhoneNumber_SentFail_ReturnsTrue()
        {
            IWebDriver driver = new ChromeDriver();
            SelectElement selectElement;
            User user = new User { FirstName = "Tamer", LastName = "Masarwe", Email = "A.B@microsoft.com", CompanyName = "Qualitest", Location = "Middle East", PhoneNumber = "0503a7223" };

            NavigateToContactUsPage(driver);

            driver.FindElement(By.Id("firstname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.FirstName);

            driver.FindElement(By.Id("lastname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.LastName);

            driver.FindElement(By.Id("company-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.CompanyName);

            driver.FindElement(By.Id("what_would_you_like_to_talk_about_0-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Click();

            driver.FindElement(By.Id("phone-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.PhoneNumber);

            selectElement = new SelectElement(driver.FindElement(By.Id("location-34dd68e0-b077-4e95-9243-b861f3f2fd7d")));
            selectElement.SelectByText(user.Location);

            driver.FindElement(By.Id("email-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.Email);
            driver.FindElement(By.CssSelector("#hsForm_34dd68e0-b077-4e95-9243-b861f3f2fd7d > div > div.actions > input")).Click();

            if (!(driver.FindElement(By.CssSelector("#hsForm_34dd68e0-b077-4e95-9243-b861f3f2fd7d > div > div.actions > input")).Displayed))
            {
                Assert.IsTrue(false);
                CleanUp(driver);
            }
            Assert.IsTrue(true);
            CleanUp(driver);
        }
        
        [Test, Category("Positive Test")]
        public void ValidUser_SentFail_ReturnsTrue()
        {
            IWebDriver driver = new ChromeDriver();
            SelectElement selectElement;
            User user = new User { FirstName = "Tamer", LastName = "Masarwe", Email = "A.B@microsoft.com", CompanyName = "Qualitest", Location = "Middle East", PhoneNumber = "050-3172231" };

            NavigateToContactUsPage(driver);
       
            driver.FindElement(By.Id("firstname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.FirstName);
            
            driver.FindElement(By.Id("lastname-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.LastName);

            driver.FindElement(By.Id("company-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.CompanyName);

            driver.FindElement(By.Id("what_would_you_like_to_talk_about_0-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).Click();

            driver.FindElement(By.Id("phone-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.PhoneNumber);

            selectElement = new SelectElement(driver.FindElement(By.Id("location-34dd68e0-b077-4e95-9243-b861f3f2fd7d")));
            selectElement.SelectByText(user.Location);

            driver.FindElement(By.Id("email-34dd68e0-b077-4e95-9243-b861f3f2fd7d")).SendKeys(user.Email);
            driver.FindElement(By.CssSelector("#hsForm_34dd68e0-b077-4e95-9243-b861f3f2fd7d > div > div.actions > input")).Click();

            if (HttpUtility.ParseQueryString(new Uri(driver.Url).Query).Equals(NavigationPage))
            {
                Assert.IsTrue(false);
                CleanUp(driver);
            }

            Assert.IsTrue(true);
            CleanUp(driver);
        }

       public void NavigateToContactUsPage(IWebDriver driver) 
        {
            driver.Navigate().GoToUrl(NavigationPage);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void CleanUp(IWebDriver driver)
        {
            driver.Quit();
        }
    }
}