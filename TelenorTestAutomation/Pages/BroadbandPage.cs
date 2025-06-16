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

        /*
         Searches for a given address, selects the first suggestion from the dropdown, and scrolls the page down.
        */
        public void SearchAddress(string address)
        {
            var searchInput = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Sök adress']")));
            searchInput.Clear();
            searchInput.SendKeys(address);
            wait.Until(d => d.FindElements(By.XPath(" //*[@data-test='address-list']")));
            Thread.Sleep(1000);
            searchInput.SendKeys(Keys.Enter);


            // Scroll down to ensure dropdowns and grids below are interactable
            var actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown).Perform();
            Thread.Sleep(3000);
    }

    /*
     Selects a random apartment number from the dropdown using keyboard navigation.
    */
    public void SelectRandomApartment()
    {
            // d is the WebDriver instance passed into the condition.
            var dropdown = wait.Until(d => d.FindElement(By.XPath("//div[@data-test='apartment-number-select']/div/select")));
            //   var dropdown = wait.Until(d => d.FindElement(By.TagName("select")));
            
            // Fetch a list of available options, from dropdown to check the length.
            var options = dropdown.FindElements(By.TagName("option")).ToList();

            // Validating the length of the list.
            if (options.Count > 1)
            {
                Random rnd = new Random();
                int maxRange = Math.Min(30, options.Count - 1); // Ensure we don't exceed available options
                int targetIndex = rnd.Next(1, maxRange + 1);    // +1 because upper bound is exclusive

                dropdown.Click(); // Open the dropdown

                // Navigate via keyboard to simulate user selection
                for (int i = 0; i < targetIndex; i++)
                {
                    dropdown.SendKeys(Keys.ArrowDown);
                    Thread.Sleep(100); 
                }

                dropdown.SendKeys(Keys.Enter); // Select
                Thread.Sleep(1000); // Wait for product grid to update
            }
    }

    
    // Checks if a product with the specified name is available on the broadband page.
    public bool IsProductAvailable(string productName)
    {
        try
        {
            //  Explicit Wait for the product grid to be populated
            // d is the WebDriver instance passed into the condition.
            wait.Until(d => d.FindElements(By.CssSelector("[data-test*='product-grid']")).Any());

            // Multi select , to retrieve all the text from listed products.
            // Where as p is the tag in html to define the text
            var allProductsText = driver.FindElements(By.CssSelector("[data-test*='product-grid']"))
            .Select(p => p.Text.ToLower());

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


