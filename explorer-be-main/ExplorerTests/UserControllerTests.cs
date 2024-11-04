using System;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.IRepository;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ExplorerTests
{
    public class UserControllerTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly Mock<IUserRepository> _repositoryMock;
        public UserControllerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            var options = new FirefoxOptions();
            _driver = new FirefoxDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

       /* [Fact]
        public void RegisterUser_Success()
        {
            _driver.Navigate().GoToUrl("http://localhost:4200/register");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.CssSelector(".form")));

            var firstNameField = _driver.FindElement(By.Id("firstName"));
            var lastNameField = _driver.FindElement(By.Id("lastName"));
            var usernameField = _driver.FindElement(By.Id("username"));
            var emailField = _driver.FindElement(By.Id("email"));
            var passwordField = _driver.FindElement(By.Id("password"));

            firstNameField.SendKeys("Test");
            lastNameField.SendKeys("User");
            usernameField.SendKeys("test");
            emailField.SendKeys("kpetkovic18@gmail.com");
            passwordField.SendKeys("test");
            var foodCheckbox = _driver.FindElement(By.XPath("//input[@name='interest3']"));
            if (!foodCheckbox.Selected)
            {
                foodCheckbox.Click();
            }

            var submitButton = _driver.FindElement(By.CssSelector(".form-submit-btn"));
            submitButton.Click();

            
            wait.Until(driver => driver.FindElement(By.CssSelector(".success-message")));
            var successMessage = _driver.FindElement(By.CssSelector(".success-message")).Text;
            Assert.Equal("User registered successfully", successMessage);


        }
*/
        public void Dispose()
        {
            _driver.Quit();
        }
    }
}