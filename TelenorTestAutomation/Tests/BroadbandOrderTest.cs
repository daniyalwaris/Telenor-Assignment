using NUnit.Framework;
using TelenorTestAutomation.Pages;
using TelenorTestAutomation.Utilities;
using OpenQA.Selenium;

namespace TelenorTestAutomation.Tests
{
    public class BroadbandOrderTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = DriverManager.GetDriver();
            driver.Navigate().GoToUrl("https://www.telenor.se/");
        }

        [Test]
        public void TestBroadbandOrderFlow()
        {
            var homePage = new HomePage(driver);    
            
            
            homePage.NavigateToBroadband();

            var broadbandPage = new BroadbandPage(driver);
            broadbandPage.SearchAddress("Storgatan 1, Uppsala");
            broadbandPage.SelectRandomApartment();
           

            Assert.That(broadbandPage.IsProductAvailable("Bredband via 5G"), Is.True,"Expected 'Bredband via 5G' to be in the results.");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Dispose();
        }
    }
}
