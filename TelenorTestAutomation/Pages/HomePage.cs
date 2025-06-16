using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace TelenorTestAutomation.Pages
{
   public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public HomePage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /*
         Navigates from the homepage to the Broadband section using the Handla menu.
        */ 
        public void NavigateToBroadband()
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);

            // Accept cookies if the popup appears
            try
            {
                var cookieAcceptBtn = wait.Until(d =>
                    d.FindElements(By.XPath("//button[contains(text(),'Godkänn alla cookies')]")).FirstOrDefault());

                if (cookieAcceptBtn != null && cookieAcceptBtn.Displayed)
                {
                    cookieAcceptBtn.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Cookie popup not visible, continuing...");
            }

            // Open "Handla" or "Produkter och tjänster" menu
            try
            {
                var menuBtn = wait.Until(d =>
                    d.FindElements(By.XPath("//*[contains(text(),'Handla') or contains(text(),'Produkter och tjänster')]"))
                    .FirstOrDefault());

                if (menuBtn != null && menuBtn.Displayed && menuBtn.Enabled)
                {
                    var actions = new Actions(driver);
                    actions.MoveToElement(menuBtn).Perform(); // Hover to trigger dropdown
                    Thread.Sleep(500);
                    menuBtn.Click(); // Open menu
                    Thread.Sleep(500);
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Main menu not found. Aborting navigation.");
                throw;
            }

            // Locate and click the Bredband (Broadband) link
            try
            {
                var broadbandLink = wait.Until(d => d.FindElements(By.XPath("(//*[contains(text(), 'Bredband')])[3]")).FirstOrDefault());
                broadbandLink.Click();
                Thread.Sleep(500);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Navigation to broadband failed due to timeout.");
                throw;
            }
        }
    }
}
