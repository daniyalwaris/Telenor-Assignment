using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TelenorTestAutomation.Utilities
{
    public static class DriverManager
    {
        private static IWebDriver? driver; // nullable driver

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("start-maximized");
                // options.AddArgument("--headless"); // optional

                driver = new ChromeDriver(options);
            }
            return driver;
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}
