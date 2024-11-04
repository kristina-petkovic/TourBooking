using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace ExplorerTests
{
   public class AuthenticationControllerTests : IDisposable
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private readonly HttpClient _client;

        public AuthenticationControllerTests()
        {
             _driver = new FirefoxDriver(); 
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:16177") 
            };
        }

        
        public void Dispose()
        {
            _driver.Quit();
        }
        [Fact]
        public void TestLogin_Success()
        {
            _driver.Navigate().GoToUrl("http://localhost:4200/login"); 

            var usernameField = _driver.FindElement(By.Name("username"));
            var passwordField = _driver.FindElement(By.Name("password"));
            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

            usernameField.SendKeys("tourist1"); 
            passwordField.SendKeys("1");
            loginButton.Click();

            _wait.Until(d => d.FindElement(By.Id("welcome"))); 
            var welcomeMessage = _driver.FindElement(By.Id("welcome")).Text;

            Assert.Contains("Welcome Back!", welcomeMessage);
        }
        
      /*  [Fact]
        public async Task TestLogin_InvalidCredentials()
        {
           
            var loginPayload = new
            {
                Username = "invalidUser",
                Password = "invalidPassword"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginPayload), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/authentication/login", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }*/

    }

}