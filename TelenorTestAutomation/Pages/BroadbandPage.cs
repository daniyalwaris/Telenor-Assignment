using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace TelenorTestAutomation.Pages
{
    public class BroadbandPage
    {
        private readonly IWebDriver driver;
        private WebDriverWait wait;

        public BroadbandPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SearchAddress(string address)
        {
            var searchInput = wait.Until(d => d.FindElement(By.CssSelector("input[placeholder*='Sök adress']")));
            searchInput.Clear();
            searchInput.SendKeys(address);

            // Wait for dropdown and select first match
            wait.Until(d => d.FindElements(By.CssSelector("ul[role='listbox'] li")).Any());
            searchInput.SendKeys(Keys.Enter);

             // ✅ Scroll down  times (natively)
            var actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown)
                .Perform();
        }



        public void SelectRandomApartment()
        {
            var dropdown = wait.Until(d => d.FindElement(By.TagName("select")));
            var options = dropdown.FindElements(By.TagName("option")).ToList();

            if (options.Count > 1)
            {
                Random rnd = new Random();
                int targetIndex = rnd.Next(1, options.Count);

                // Focus on dropdown
                dropdown.Click();

                // Use keyboard to move to desired option
                for (int i = 0; i < targetIndex; i++)
                {
                    dropdown.SendKeys(Keys.ArrowDown);
                    Thread.Sleep(100); // slight delay to simulate real key press
                }

                dropdown.SendKeys(Keys.Enter); // Confirm selection
                Thread.Sleep(500); // Wait for backend call to trigger and page to update
            }
        }

        /*public void SelectRandomApartment()
        {
            var dropdown = wait.Until(d => d.FindElement(By.TagName("select")));
            dropdown.Click();
            var options = dropdown.FindElements(By.TagName("option")).ToList();

            if (options.Count > 1)
            {
                Random rnd = new Random();
                int index = rnd.Next(1, options.Count); // skip index 0 if it's a label
                new SelectElement(dropdown).SelectByIndex(index);
            }
        }
        */

        public bool IsProductAvailable(string productName)
        {
            try
            {
                // Wait for product grid to load
                wait.Until(d => d.FindElements(By.CssSelector("[data-test*='product-grid']")).Any());

                var allProductsText = driver
                    .FindElements(By.CssSelector("[data-test*='product-grid']"))
                    .Select(p => p.Text.ToLower());

                Console.WriteLine("Products found:");
                foreach (var product in allProductsText)
                {
                    Console.WriteLine(" - " + product);
                }

                return allProductsText.Any(t => t.Contains(productName.ToLower()));
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("❌ Timed out waiting for product cards to load.");
                return false;
            }
        }

    }
}


