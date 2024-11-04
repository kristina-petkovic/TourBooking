using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ExplorerTests
{
    public class TourControllerSeleniumTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public TourControllerSeleniumTests()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

      /*  [Fact]
        public void CreateTour_ShouldCreateNewTour()
        {
            _driver.Navigate().GoToUrl("http://localhost:4200/login");

            var usernameField = _driver.FindElement(By.Id("username"));
            var passwordField = _driver.FindElement(By.Id("password"));
            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

            usernameField.SendKeys("author2");
            passwordField.SendKeys("2");
            loginButton.Click();

            _driver.Navigate().GoToUrl("http://localhost:4200/create-tour");

            var nameField = _driver.FindElement(By.Id("name"));
            var descriptionField = _driver.FindElement(By.Id("description"));
            var ticketCountField = _driver.FindElement(By.Id("ticketCount"));
            var priceField = _driver.FindElement(By.Id("price"));
            var difficultyDropdown = new SelectElement(_driver.FindElement(By.Id("difficulty")));
            var createButton = _driver.FindElement(By.CssSelector(".btn.btn-primary"));

            nameField.SendKeys("New Tour");
            descriptionField.SendKeys("odlicna turica.");
            ticketCountField.SendKeys("100");
            priceField.SendKeys("50");
            difficultyDropdown.SelectByValue("easy"); 

            createButton.Click();

            var successMessage = _driver.FindElement(By.CssSelector(".success-message"));
            Assert.NotNull(successMessage);
            Assert.Equal("Tour created successfully!", successMessage.Text);
        }

        [Fact]
        public void PublishTour_ShouldChangeTourStatusToPublished()
        {
            _driver.Navigate().GoToUrl("http://localhost:4200/login");

            var usernameField = _driver.FindElement(By.Id("username"));
            var passwordField = _driver.FindElement(By.Id("password"));
            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

            usernameField.SendKeys("author2");
            passwordField.SendKeys("2");
            loginButton.Click();

            _driver.Navigate().GoToUrl("http://localhost:4200/all-tours");

            var publishButton = _driver.FindElement(By.Id("publishbutton"))); 
            publishButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var statusLabel = wait.Until(d => d.FindElement(By.Id("published")));
            
            Assert.Equal("Published", statusLabel.Text);
        }*/
    }
}
