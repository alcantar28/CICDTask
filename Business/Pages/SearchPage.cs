using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using CICD.Core.Factory;
using CICD.Core.Utilities;

namespace CICD.Business.Pages
{
    public class SearchPage: BasePage
    {
        private readonly By searchIconLocator = By.XPath("//span[@class='search-icon dark-icon header-search__search-icon']"); //En el <span>, no <button> (?)
        private readonly By globalFindButtonLocator = By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button");
        private readonly By articleHeadingsLocator = By.CssSelector("article[class='search-results__item'] > h3 > a");
        private readonly By articleDescriptionLocator = By.CssSelector("article[class='search-results__item'] > p");
        private readonly By searchInputLocator = By.Name("q");

        public SearchPage() : base(DriverManager.GetInstance())
        { }

        public void WaitForSearchPanelToLoad()
        {
            IWebElement searchIcon = ElementInteractions.GetElement(driver, searchIconLocator);
            Actions action = new Actions(driver);
            ElementWaits.WaitForClickableElement(driver, searchIconLocator); 

            try
            {
                action.Click(searchIcon)  //Esperar a que el panel de búsqueda esté completamente cargado
                    .Pause(TimeSpan.FromSeconds(1))
                    .Perform();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public void TypeKeywords(string keywords)
        {
            try
            {
                var searchInput = ElementInteractions.GetElement(driver, searchInputLocator);
                searchInput.SendKeys(keywords);
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        public void SearchForKeywords()
        {
            try { 
                var findButton = ElementWaits.WaitForClickableElement(driver, globalFindButtonLocator);
                findButton.Click();
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
        
        public void AssertSearchResults(string keywords)
        {
            try { 
                string cleanKeywords = keywords.Replace("\"", "");
                string[] separatedKeywords = cleanKeywords.Split("/");

                var articleHeadings = ElementInteractions.GetElements(driver, articleHeadingsLocator);
                var articleDescription = ElementInteractions.GetElements(driver, articleDescriptionLocator);
                foreach (var headingElement in articleHeadings)
                {
                    foreach (var descrElement in articleDescription)
                    {
                        bool headingOrDescriptionContainAnyKeyword = separatedKeywords.Any(word =>
                            !string.IsNullOrEmpty(word)
                            && (headingElement.Text.Contains(word, StringComparison.OrdinalIgnoreCase)
                            || descrElement.Text.Contains(word, StringComparison.OrdinalIgnoreCase)));
                        Assert.That(headingOrDescriptionContainAnyKeyword,
                            $"Heading '{headingElement.Text}' or description '{descrElement.Text}' do not contain any of the specified keywords");
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileCreator.LogGeneralError(MethodBase.GetCurrentMethod().Name, ex);
                throw new Exception();
            }
        }
    }
}
